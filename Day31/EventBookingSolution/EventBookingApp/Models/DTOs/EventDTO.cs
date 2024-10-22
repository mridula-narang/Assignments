namespace EventBookingApp.Models.DTOs
{
    public class EventDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
