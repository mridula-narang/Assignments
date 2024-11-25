namespace HotelApp.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public TimeSpan StandardCheckIn { get; set; }
        public TimeSpan StandardCheckOut { get; set; }
        public int NumberOfRooms { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public ICollection<Amenities> Amenities { get; set; }
    }
}
