using EventBookingApp.Contexts;
using EventBookingApp.Exceptions;
using EventBookingApp.Interfaces;
using EventBookingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventBookingApp.Repositories
{
    public class BookingRepository : IRepository<int, Booking>
    {
        private readonly EventContext _context;
        private readonly ILogger<EventRepository> _logger;

        public BookingRepository(EventContext eventContext, ILogger<EventRepository> logger)
        {
            _context = eventContext;
            _logger = logger;
        }
        public async Task<Booking> Add(Booking entity)
        {
            try
            {
                var bookingEntry = _context.Bookings.Add(entity);
                await _context.SaveChangesAsync();
                return bookingEntry.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding event");
                throw new CouldNotAddException("Event");
            }
        }

        public async Task<Booking> Delete(int key)
        {
            var bookingEntity = await _context.Bookings.FirstOrDefaultAsync(e => e.Id == key);
            if (bookingEntity == null)
            {
                throw new NotFoundException("Event");
            }
            _context.Bookings.Remove(bookingEntity);
            await _context.SaveChangesAsync();
            return bookingEntity;
        }

        public async Task<Booking> Get(int key)
        {
            var bookingEntity = await _context.Bookings.FirstOrDefaultAsync(e => e.Id == key); 
            if (bookingEntity == null)
            {
                throw new CollectionEmptyException("Products");
            }
            return bookingEntity;
        }

        public async Task<IEnumerable<Booking>> GetAll()
        {
            var bookingEntity = await _context.Bookings.ToListAsync();
            if (bookingEntity.Count == 0)
            {
                throw new CollectionEmptyException("Products");
            }
            return bookingEntity;
        }

        public async Task<Booking> Update(int key, Booking entity)
        {
            var bookingEntity = await Get(key);
            if (bookingEntity == null)
            {
                throw new NotFoundException("Event");
            }
            bookingEntity.EventId = entity.EventId;
            bookingEntity.EmployeeId = entity.EmployeeId;
            bookingEntity.BookingDate = entity.BookingDate;
            await _context.SaveChangesAsync();
            return bookingEntity;
        }
    }
}
