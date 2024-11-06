using HotelBookingApp.Contexts;
using HotelBookingApp.Exceptions;
using HotelBookingApp.Interfaces;
using HotelBookingApp.Models;
using HotelBookingApp.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApp.Repositories
{
    public class RoomRepository : IRepository<int, Room>
    {
        private readonly HotelContext _context;
        private readonly ILogger<RoomRepository> _logger;

        public RoomRepository(HotelContext context, ILogger<RoomRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Room> Add(Room entity)
        {
            try
            {
                _context.Rooms.Add(entity);
                await _context.SaveChangesAsync();
                return entity;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not add Room");
                throw new CouldNotAddException("Room");
            }
        }

        public async Task<Room> Delete(int key)
        {
            var room = await Get(key);
            if (room == null)
            {
                throw new NotFoundException("Room");
            }
            try
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
                return room;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not delete room");
                throw new Exception("Unable to delete");
            }
        }
        public async Task<Room> Get(int roomId)
        {
                var room = await _context.Rooms.FindAsync(roomId);
                if (room == null)
                {
                    throw new NotFoundException("Room");
                }
                return room;
        }

        public async Task<IEnumerable<Room>> GetAll()
        {
            var rooms = await _context.Rooms.ToListAsync();
            if (rooms.Count == 0)
            {
                throw new CollectionEmptyException("Room");
            }
            return rooms;
        }

        public async Task<Room> Update(int key, Room entity)
        {
           var room = await Get(key);
            if (room == null)
            {
                throw new NotFoundException("Room");
            }
            try
            {
                room.Name = entity.Name;
                room.Type = entity.Type;
                room.Price = entity.Price;
                room.IsBooked = entity.IsBooked;
                room.Features = entity.Features;
                room.Capacity = entity.Capacity;
                await _context.SaveChangesAsync();
                return room;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not update room");
                throw new Exception("Unable to update");
            }
        }
    }
}
