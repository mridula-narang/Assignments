using AutoMapper;
using HotelBookingApp.Exceptions;
using HotelBookingApp.Interfaces;
using HotelBookingApp.Models;
using HotelBookingApp.Models.DTOs;
using HotelBookingApp.Repositories;

namespace HotelBookingApp.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<int, Booking> _bookingRepository;
        private readonly IRepository<int, Room> _roomRepository; // Add this line
        private readonly IMapper _mapper;

        public BookingService(IRepository<int, Booking> bookingRepository, IRepository<int, Room> roomRepository, IMapper mapper) // Modify constructor
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository; // Add this line
            _mapper = mapper;
        }

        public async Task<Booking> AddBooking(BookingDTO bookingDTO)
        {
            var booking = _mapper.Map<Booking>(bookingDTO);

            // Calculate TotalPrice based on the room price and duration
            booking.TotalPrice = await CalculateTotalPrice(bookingDTO);

            // Set a default status if none is provided in DTO
            booking.Status = bookingDTO.Status;

            // Save booking
            var addedBooking = await _bookingRepository.Add(booking);
            return addedBooking;
        }

        public async Task<decimal> CalculateTotalPrice(BookingDTO bookingDTO)
        {
            var booking = _mapper.Map<Booking>(bookingDTO);
            var room = await _roomRepository.Get(booking.RoomId); // Retrieve room to get the price
            var duration = (booking.CheckOutDate - booking.CheckInDate).Days;

            return room.Price * duration;
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
            var updatedBooking = await _bookingRepository.Update(bookingId, booking);
            return updatedBooking;
        }
    }
}
