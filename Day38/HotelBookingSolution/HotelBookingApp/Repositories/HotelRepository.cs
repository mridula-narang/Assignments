using AutoMapper;
using HotelBookingApp.Contexts;
using HotelBookingApp.Exceptions;
using HotelBookingApp.Interfaces;
using HotelBookingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApp.Repositories
{
    public class HotelRepository : IRepository<int, Hotel>
    {
        private readonly HotelContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<HotelRepository> _logger;

        public HotelRepository(HotelContext context, IMapper mapper,ILogger<HotelRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;

        }

        public async Task<Hotel> Add(Hotel entity)
        {
            _context.Hotels.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Hotel> Delete(int key)
        {
            var hotel = await Get(key);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
            }
            return hotel;
        }

        public Task<Hotel> Get(int key)
        {
            var hotel = _context.Hotels.FirstOrDefaultAsync(r => r.HotelId == key);
            return hotel;
        }

        public async Task<IEnumerable<Hotel>> GetAll()
        {
            var hotels = await _context.Hotels.ToListAsync();
            if (hotels.Count == 0)
            {
                throw new NotFoundException("hotel");
            }
            return hotels;
        }

        public async Task<Hotel> Update(int key, Hotel entity)
        {
            var hotel = await Get(key);
            if (hotel == null)
            {
                throw new NotFoundException("hotel");
            }
            try
            {
                hotel.Amenities = entity.Amenities;
                hotel.Location = entity.Location;
                hotel.Rooms = entity.Rooms;
                hotel.StandardCheckIn = entity.StandardCheckIn;
                hotel.StandardCheckOut = entity.StandardCheckOut;
                return hotel;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not update hotel");
                throw new Exception("Unable to update");
            }
        }
    }
}
