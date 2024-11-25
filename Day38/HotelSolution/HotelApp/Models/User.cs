namespace HotelApp.Models
{
    public class User
    {
        public enum Roles
        {
            Admin,
            Customer
        }
        public int UserId { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; } // Changed to byte[] to match the usage in UserService
        public byte[] HashKey { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public User.Roles Role { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
