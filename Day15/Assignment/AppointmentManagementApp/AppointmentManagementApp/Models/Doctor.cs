namespace AppointmentManagementApp.Models
{
    public class Doctor : IEquatable<Doctor>
    {
        public int DoctorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public bool Equals(Doctor? other)
        {
            return this.Username == (other ?? new Doctor()).Username;
        }
    }
}
