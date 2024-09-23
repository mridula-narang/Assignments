using ClinicManagementApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementApp.Controllers
{
    public class DoctorController : Controller
    {
        List<Doctor> doctors = new List<Doctor>
        {
            new Doctor { Id = 1, Name = "Dr. Theodore Altman", PhoneNumber = "7795785774", Department = "Cardiology", Specialization = "Cardiothoracic Surgeon", Image = "/images/1.jpg" },
            new Doctor { Id = 2, Name = "Dr. Meredith Grey", PhoneNumber = "8867587848", Department = "Neurology", Specialization = "Neuro-oncologist" ,Image = "/images/2.jpg"},
            new Doctor { Id = 3, Name = "Dr. Miranda Bailey", PhoneNumber = "8869577677", Department = "Ophthalmology", Specialization = "Retina", Image = "/images/3.jpg" },
        };
        public IActionResult Index()
        {
            return View(doctors);
        }
    }
}
