using AutoMapper;
using HotelBookingApp.Contexts;
using HotelBookingApp.Exceptions;
using HotelBookingApp.Interfaces;
using HotelBookingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApp.Repositories
{
    public class PaymentRepository : IRepository<int, Payment>
    {
        private readonly HotelContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<HotelRepository> _logger;

        public PaymentRepository(HotelContext context, IMapper mapper, ILogger<HotelRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;

        }
        public async Task<Payment> Add(Payment entity)
        {
            _context.Payments.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Payment> Delete(int key)
        {
            var hotel = await Get(key);
            if (hotel != null)
            {
                _context.Payments.Remove(hotel);
                await _context.SaveChangesAsync();
            }
            return hotel;
        }

        public async Task<Payment> Get(int key)
        {
            var hotel = await _context.Payments.FirstOrDefaultAsync(r => r.PaymentId == key);
            if (hotel == null)
            {
                throw new NotFoundException("hotel");
            }
            return hotel;
        }

        public async Task<IEnumerable<Payment>> GetAll()
        {
            var hotels = await _context.Payments.ToListAsync();
            if (hotels.Count == 0)
            {
                throw new CollectionEmptyException("hotel");
            }
            return hotels;
        }

        public async Task<Payment> Update(int key, Payment entity)
        {
            var payment = await Get(key);
            if (payment == null)
            {
                throw new NotFoundException("hotel");
            }
            try
            {
                _context.Payments.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not update hotel");
                throw new Exception("Unable to update");
            }
        }
    }
}
