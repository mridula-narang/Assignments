using AppointmentManagementApp.Interfaces;
using AppointmentManagementApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentManagementApp.Controllers
{
    public class AppointmentController : Controller
    {
        private IAppointmentService _appointmentService;
        private ILoginService _loginService;
        private IRepository<string, Doctor> _doctorRepository;

        public AppointmentController(IAppointmentService appointmentService, ILoginService loginService, IRepository<string, Doctor> doctorRepository)
        {
            _appointmentService = appointmentService;
            _loginService = loginService;
            _doctorRepository = doctorRepository;
        }

        public IActionResult BookAppointment()
        {
            var doctors = _doctorRepository.GetAll();
            return View(doctors); // Pass doctors to view for selection
        }

        [HttpPost]
        public IActionResult BookAppointment(int doctorId, DateTime appointmentDateTime)
        {
            int patientId = GetLoggedInPatientId(); // Get the patient ID from the logged-in user
            try
            {
                _appointmentService.BookAppointment(patientId, doctorId, appointmentDateTime);
                ViewBag.SuccessMessage = "Appointment booked successfully!";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }

            var doctors = _doctorRepository.GetAll();
            return View(doctors);
        }

        private int GetLoggedInPatientId()
        {
            // Assuming you stored the patient ID in the session during login
            var patientId = HttpContext.Session.GetInt32("PatientId");

            if (patientId.HasValue)
            {
                return patientId.Value;
            }

            throw new Exception("Patient is not logged in.");
        }

    }
}
