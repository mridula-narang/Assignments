using AutoMapper;
using HotelBookingApp.Interfaces;
using HotelBookingApp.Models;
using HotelBookingApp.Models.DTOs;
using HotelBookingApp.Repositories;

namespace HotelBookingApp.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRepository<int, Room> _roomRepository;
        private readonly IMapper _mapper;

        public RoomService(IRepository<int,Room> roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }
        public async Task<Room> AddRoom(RoomDTO roomDTO)
        {
            var room = _mapper.Map<Room>(roomDTO);
            var addedRoom = await _roomRepository.Add(room);
            return addedRoom;
        }

        public async Task<Room> DeleteRoom(int id)
        {
            var room = await _roomRepository.Get(id);
            if (room == null)
            {
                return null;
            }
            var deletedRoom = await _roomRepository.Delete(id);
            return room;
        }

        public async Task<Room> GetRoom(int id)
        {
            var room = await _roomRepository.Get(id);
            return room;
        }

        public async Task<Room> UpdateRoom(int id, RoomDTO roomDTO)
        {
            var room = await _roomRepository.Get(id);
            if (room == null)
            {
                return null;
            }
            var updatedRoom = _mapper.Map(roomDTO, room);
            await _roomRepository.Update(id, updatedRoom);
            return updatedRoom;
        }

        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            var rooms = await _roomRepository.GetAll();
            return rooms;
        }
    }
}
