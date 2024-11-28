namespace AppointmentManagementApp.Models
{
    public class Appointment : IEquatable<Appointment>
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }

        public bool Equals(Appointment other)
        {
            return this.AppointmentId == other.AppointmentId;
        }
    }
}
