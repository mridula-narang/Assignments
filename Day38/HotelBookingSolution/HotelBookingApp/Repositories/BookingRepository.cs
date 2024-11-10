using HotelBookingApp.Contexts;
using HotelBookingApp.Exceptions;
using HotelBookingApp.Interfaces;
using HotelBookingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApp.Repositories
{
    public class BookingRepository : IRepository<int, Booking>
    {
        private HotelContext _context;
        private ILogger<BookingRepository> _logger;

        public BookingRepository(HotelContext context, ILogger<BookingRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Booking> Add(Booking entity)
        {
            //var roomExists = await _context.Rooms.AnyAsync(r => r.RoomId == entity.RoomId);
            //if (!roomExists)
            //{
            //    throw new NotFoundException("Room not found");
            //}

            try
            {
                _context.Bookings.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not add Booking");
                throw new CouldNotAddException("Booking");
            }
        }

        public async Task<Booking> Delete(int key)
        {
            var booking = await Get(key);
            if (booking == null)
            {
                throw new NotFoundException("Booking");
            }
            try
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
                return booking;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not delete booking");
                throw new Exception("Unable to delete");
            }
        }

        public async Task<Booking> Get(int key)
        {
            try
            {
                var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == key);
                return booking;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not get room");
                throw new NotFoundException("Room");
            }
        }

        public async Task<IEnumerable<Booking>> GetAll()
        {
            var bookings = await _context.Bookings.ToListAsync();
            if (bookings.Count == 0)
            {
                throw new NotFoundException("Booking");
            }
            return bookings;
        }

<<<<<<< HEAD
        public Task<Booking> GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

=======
>>>>>>> 76a83b798404e0228ee30b6390690c0b63af6e2e
        public async Task<Booking> Update(int key, Booking entity)
        {
            var booking = await Get(key);
            if (booking == null)
            {
                throw new NotFoundException("Booking");
            }
            try
            {
<<<<<<< HEAD
                //booking.HotelId = entity.HotelId;
                //booking.RoomId = entity.RoomId;
                //booking.UserId = entity.UserId;
                //booking.CheckInDate = entity.CheckInDate; 
                //booking.CheckOutDate = entity.CheckOutDate; 
                //booking.NumberOfGuests = entity.NumberOfGuests;
                //booking.TotalPrice = entity.TotalPrice;
=======
                booking.HotelId = entity.HotelId;
                booking.RoomId = entity.RoomId;
                booking.UserId = entity.UserId;
                booking.CheckInDate = entity.CheckInDate; 
                booking.CheckOutDate = entity.CheckOutDate; 
                booking.NumberOfGuests = entity.NumberOfGuests;
                booking.TotalPrice = entity.TotalPrice;
>>>>>>> 76a83b798404e0228ee30b6390690c0b63af6e2e
                booking.Status = entity.Status;

                await _context.SaveChangesAsync();
                return booking;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not update booking");
                throw new Exception("Unable to update");
            }
        }
    }
}
