using HotelBookingApp.Models.DTOs;

namespace HotelBookingApp.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(UserTokenDTO user);
    }
}
