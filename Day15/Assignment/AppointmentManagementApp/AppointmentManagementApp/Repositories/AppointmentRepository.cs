using AppointmentManagementApp.Exceptions;
using AppointmentManagementApp.Interfaces;
using AppointmentManagementApp.Models;

namespace AppointmentManagementApp.Repositories
{
    public class AppointmentRepository : IRepository<int, Appointment>
    {
        private List<Appointment> _appointments = new List<Appointment>();
        public Appointment Add(Appointment item)
        {
            if (!IsOverlapping(item))
            {
                _appointments.Add(item);
                return item;
            }
            throw new AppointmentOverlapException();
        }

        private bool IsOverlapping(Appointment newAppointment)
        {
            return _appointments.Any(a =>
               a.DoctorId == newAppointment.DoctorId &&
               a.AppointmentDate == newAppointment.AppointmentDate);
        }

        public Appointment Delete(int key)
        {
            var appointment = Get(key);
            _appointments.Remove(appointment);
            return appointment;

        }

        public Appointment Get(int key)
        {
            var appointment = _appointments.FirstOrDefault(a => a.AppointmentId == key);
            if (appointment == null)
            {
                throw new AppointmentNotFoundException();
            }
            return appointment;
        }

        public List<Appointment> GetAll()
        {
            if (_appointments.Count == 0)
            {
                throw new NoAppointmentsException();
            }
            return _appointments;
        }

        public Appointment Update(Appointment item)
        {
            var existingAppointment = Get(item.AppointmentId);
            existingAppointment.DoctorId = item.DoctorId;
            existingAppointment.AppointmentDate = item.AppointmentDate;
            return existingAppointment;
        }
    }
}
