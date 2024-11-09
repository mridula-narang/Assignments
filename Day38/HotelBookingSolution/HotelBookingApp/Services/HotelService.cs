using AutoMapper;
using HotelBookingApp.Interfaces;
using HotelBookingApp.Models;
using HotelBookingApp.Models.DTOs;
using HotelBookingApp.Repositories;

namespace HotelBookingApp.Services
{
    public class HotelService : IHotelService
    {
        private readonly IRepository<int, Hotel> _hotelRepository;
        private readonly IMapper _mapper;

        public HotelService(IRepository<int, Hotel> hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<Hotel> AddHotel(HotelDTO hotelDTO)
        {
            var hotel = _mapper.Map<Hotel>(hotelDTO);
            var addedHotel = await _hotelRepository.Add(hotel);
            return addedHotel;
        }

        public async Task<Hotel> DeleteHotel(int id)
        {
            var hotel = await _hotelRepository.Get(id);
            if (hotel == null)
            {
                return null;
            }
            var deletedHotel = _hotelRepository.Delete(id);
            return hotel;
        }

        public async Task<IEnumerable<Hotel>> GetAllHotel()
        {
            var hotels = await _hotelRepository.GetAll();
            return hotels;
        }

        public async Task<Hotel> GetHotel(int id)
        {
            var hotel = await _hotelRepository.Get(id);
            return hotel;
        }

        public async Task<Hotel> UpdateHotel(int id, HotelDTO hotelDTO)
        {
            var hotel = await _hotelRepository.Get(id);
            if (hotel == null)
            {
                return null;
            }
            var updatedHotel = _mapper.Map(hotelDTO, hotel);
            await _hotelRepository.Update(id, updatedHotel);
            return updatedHotel;
        }
    }
}
