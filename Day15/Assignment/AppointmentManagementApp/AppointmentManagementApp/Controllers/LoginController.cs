using Microsoft.AspNetCore.Mvc;
using AppointmentManagementApp.Interfaces;

namespace AppointmentManagementApp.Controllers
{
    public class LoginController : Controller
    {
        private ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public IActionResult PatientLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PatientLogin(string username, string password)
        {
            if (_loginService.Login(username, password))
            {
                // Get the logged-in patient details
                var patient = _loginService.GetPatientByUsername(username);

                if (patient != null)
                {
                    // Store the PatientId in session
                    HttpContext.Session.SetInt32("PatientId", patient.PatientId);
                }

                return RedirectToAction("BookAppointment", "Appointment");
            }

            ViewBag.ErrorMessage = "Invalid username or password";
            return View();
        }

    }
}
