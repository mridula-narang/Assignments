﻿using static HotelBookingApp.Models.Room;

namespace HotelBookingApp.Models.DTOs
{
    public class RoomDTO
    {
        public int RoomId { get; set; }
        public int HotelId { get; set; }
        public string Name { get; set; } = string.Empty;
        public RoomType Type { get; set; }
        public decimal Price { get; set; }
        public RoomStatus IsBooked { get; set; }
        public string Features { get; set; } = string.Empty;
        public int Capacity { get; set; }

    }
}
