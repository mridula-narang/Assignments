namespace AppointmentManagementApp.Models
{
    public class Patient : IEquatable<Patient>
    {
        public int PatientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Disease { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public bool Equals(Patient? other)
        {
            return this.Username == (other ?? new Patient()).Username;
        }

    }
}
