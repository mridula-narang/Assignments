using AutoMapper;
using HotelBookingApp.Exceptions;
using HotelBookingApp.Interfaces;
using HotelBookingApp.Models.DTOs;
using HotelBookingApp.Models;

namespace HotelBookingApp.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<int, Booking> _bookingRepository;
        private readonly IRepository<int, Room> _roomRepository; // Add this line
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IRepository<int, User> _userRepository;
        private readonly IRoomService _roomService;

        public BookingService(IRepository<int, Booking> bookingRepository, IRepository<int, Room> roomRepository, IMapper mapper, IEmailService emailService, IRepository<int, User> userRepository, IRoomService roomService)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _mapper = mapper;
            _emailService = emailService;
            _userRepository = userRepository;
            _roomService = roomService;
        }
        public BookingService(IRepository<int, Booking> bookingRepository, IRepository<int, Room> roomRepository, IMapper mapper) // Modify constructor
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository; // Add this line
            _mapper = mapper;
        }

        public async Task<Booking> AddBooking(BookingDTO bookingDTO)
        {
            var booking = _mapper.Map<Booking>(bookingDTO);

            // Calculate TotalPrice
            booking.TotalPrice = await CalculateTotalPrice(bookingDTO);

            // Save booking
            var addedBooking = await _bookingRepository.Add(booking);
            return addedBooking;
        }

        public async Task<decimal> CalculateTotalPrice(BookingDTO bookingDTO)
        {
            
                if (bookingDTO.RoomId <= 0 || bookingDTO.Quantity <= 0)
                {
                    throw new InvalidOperationException("Invalid room selection or quantity.");
                }

                if (bookingDTO.CheckInDate >= bookingDTO.CheckOutDate)
                {
                    throw new ArgumentException("Invalid booking dates.");
                }

                var room = await _roomRepository.Get(bookingDTO.RoomId);
                if (room == null)
                {
                    throw new CollectionEmptyException($"Room with ID {bookingDTO.RoomId}");
                }

                // Calculate duration of stay
                var duration = (bookingDTO.CheckOutDate - bookingDTO.CheckInDate).Days;

                // Total price = Room price * Quantity * Duration
                var totalPrice = room.Price * bookingDTO.Quantity * duration;

                return totalPrice;

        }

        public async Task<Booking> DeleteBooking(int id)
        {
            var booking = await _bookingRepository.Get(id);
            if (booking == null)
            {
                throw new CollectionEmptyException("Booking");
            }
            var deletedBooking = await _bookingRepository.Delete(id);
            return booking;
        }

        public async Task<IEnumerable<Booking>> GetBookings()
        {
            var bookings = await _bookingRepository.GetAll();
            return bookings;
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUserId(int userId)
        {
            var bookings = await _bookingRepository.GetAll();
            return bookings.Where(b => b.UserId == userId);
        }

        public async Task<Booking> UpdateBooking(BookingDTO bookingDTO)
        {
            var booking = _mapper.Map<Booking>(bookingDTO);
            var updatedBooking = await _bookingRepository.Update(booking.BookingId, booking);
            return updatedBooking;
        }

        public async Task<Booking> UpdateBookingStatus(int bookingId, Booking.BookingStatus status)
        {
            var booking = await _bookingRepository.Get(bookingId);
            if (booking == null)
            {
                throw new CollectionEmptyException("Booking");
            }
            booking.Status = status;

            if (status == Booking.BookingStatus.Confirmed)
            {
                var room = await _roomRepository.Get(booking.RoomId);
                if (room != null && room.IsBooked != Room.RoomStatus.Booked)
                {
                    room.IsBooked = Room.RoomStatus.Booked;
                    await _roomRepository.Update(room.RoomId, room); // Update room status in the repository
                }
            }
            else if (status == Booking.BookingStatus.Cancelled)
            {
                var room = await _roomRepository.Get(booking.RoomId);
                if (room != null && room.IsBooked == Room.RoomStatus.Booked)
                {
                    room.IsBooked = Room.RoomStatus.Available;
                    await _roomRepository.Update(room.RoomId, room);
                }
            }

            var updatedBooking = await _bookingRepository.Update(bookingId, booking);
            if (status == Booking.BookingStatus.Confirmed || status == Booking.BookingStatus.Cancelled)
            {
                string subject = $"Booking Status Updated to {status}";
                string message = $"Your booking has been {status.ToString().ToLower()}.";
                var user = await _userRepository.Get(booking.UserId);
                if (user == null)
                {
                    throw new InvalidOperationException("User information is missing.");
                }
                await _emailService.SendStatusChangeEmail(booking.Users.Email, subject, message);
            }
            return updatedBooking;
        }

        public async Task<IEnumerable<RoomDTO>> GetAvailableRoomsForBooking()
        {
            return await _roomService.GetAvailableRooms();
        }

    }
}