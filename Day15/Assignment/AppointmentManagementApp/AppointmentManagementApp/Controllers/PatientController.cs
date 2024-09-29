using Microsoft.AspNetCore.Mvc;

namespace AppointmentManagementApp.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
