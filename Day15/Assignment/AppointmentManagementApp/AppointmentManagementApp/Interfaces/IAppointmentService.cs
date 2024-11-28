using AppointmentManagementApp.Models;

namespace AppointmentManagementApp.Interfaces
{
    public interface IAppointmentService
    {
        public Appointment BookAppointment(int patientId, int doctorId, DateTime appointmentDate);
    }
}
