namespace EventBookingApp.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime BookingDate { get; set; }
        public IEnumerable<Event> Events { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public Employee Employee { get; set; }
        public Event Event { get; set; }

        public Booking()
        {
            Events = new List<Event>();
            Employees = new List<Employee>();
        }
    }
}
