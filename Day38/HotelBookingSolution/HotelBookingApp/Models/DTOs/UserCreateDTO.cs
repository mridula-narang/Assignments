﻿using static HotelBookingApp.Models.User;
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
        public Roles Role { get; set; }
    }
}
