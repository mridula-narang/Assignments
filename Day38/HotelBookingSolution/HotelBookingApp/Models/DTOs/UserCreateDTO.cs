using static HotelBookingApp.Models.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using HotelBookingApp.Misc;

namespace HotelBookingApp.Models.DTOs
{
    public class UserCreateDTO
    {
        [UsernameValidator]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password cannot be empty")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Password pattern worng")]
        [DefaultValue("password")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Role cannot be empty")]
<<<<<<< HEAD
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
=======
>>>>>>> 76a83b798404e0228ee30b6390690c0b63af6e2e
        public Roles Role { get; set; }
    }
}
