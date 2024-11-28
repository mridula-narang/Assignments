namespace HotelApp.Models.DTOs
{
    public class HotelDTO
    {
        public string Location { get; set; }
        public string Name { get; set; }
        public TimeSpan StandardCheckIn { get; set; }
        public TimeSpan StandardCheckOut { get; set; }
        public int NumberOfRooms { get; set; }
    }
}
