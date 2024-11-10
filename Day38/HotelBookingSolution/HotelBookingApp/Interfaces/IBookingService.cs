using HotelBookingApp.Models;
using HotelBookingApp.Models.DTOs;
using static HotelBookingApp.Models.Booking;

namespace HotelBookingApp.Interfaces
{
    public interface IBookingService
    {
        Task<Booking> AddBooking(BookingDTO bookingDTO);
        Task<Booking> DeleteBooking(int id);
        Task<IEnumerable<Booking>> GetBookings();
        Task<IEnumerable<Booking>> GetBookingsByUserId(int userId);
        Task<Booking> UpdateBooking(BookingDTO bookingDTO);
        //update booking status
        Task<Booking> UpdateBookingStatus(int bookingId, BookingStatus status);


    }
}
