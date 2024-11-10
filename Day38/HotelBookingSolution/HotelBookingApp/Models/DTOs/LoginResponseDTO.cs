namespace HotelBookingApp.Models.DTOs
{
    public class LoginResponseDTO
    {
        public string Username { get; set; }
        public string Token { get; set; } // Add this property
        public string Email { get; set; }
    }
}
