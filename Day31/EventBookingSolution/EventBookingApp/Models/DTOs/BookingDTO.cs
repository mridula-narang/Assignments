namespace EventBookingApp.Models.DTOs
{
    public class BookingDTO
    {
        public int EventId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
