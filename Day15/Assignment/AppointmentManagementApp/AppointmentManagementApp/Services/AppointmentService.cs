using AppointmentManagementApp.Interfaces;
using AppointmentManagementApp.Models;

namespace AppointmentManagementApp.Services
{
    public class AppointmentService : IAppointmentService
    {
        private IRepository<int, Appointment> _appointmentRepository;

        public AppointmentService(IRepository<int, Appointment> appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public Appointment BookAppointment(int patientId, int doctorId, DateTime appointmentDate)
        {
            Appointment appointment = new Appointment
            {
                PatientId = patientId,
                DoctorId = doctorId,
                AppointmentDate = appointmentDate
            };

            return _appointmentRepository.Add(appointment);
        }
    }
}
