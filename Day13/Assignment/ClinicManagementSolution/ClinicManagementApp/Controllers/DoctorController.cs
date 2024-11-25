using ClinicManagementApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;

namespace ClinicManagementApp.Controllers
{
    public class DoctorController : Controller
    {
        private IHostingEnvironment Environment;
        public DoctorController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }


        private static List<Doctor> _doctors = new List<Doctor>
        {
            new Doctor { DoctorId = 1, Name = "Dr. John Smith",Specialty = "Cardiology", Department = "Cardiology",Phone = "123-456-7890", YearsOfExperience = 10, ImagePath = "doctor1.jpg" },
            new Doctor { DoctorId = 2, Name = "Dr. Emily White", Specialty = "Neurology", Department = "Neurology",Phone = "987-654-3210", YearsOfExperience = 7, ImagePath = "doctor2.jpg" },
            new Doctor { DoctorId = 3, Name = "Dr. Mark Brown", Specialty = "Pediatrics", Department = "Pediatrics",Phone = "555-555-5555", YearsOfExperience = 15, ImagePath = "doctor3.jpg" }
        };


        // Display all doctors
        public IActionResult Index()
        {
            return View(_doctors);
        }

        // Create doctor (GET)
        public IActionResult Create()
        {
            return View();
        }

        // Create doctor (POST)
        [HttpPost]
        public IActionResult Create(Doctor doctor, IFormFile imageFile)
        {
            if (!ModelState.IsValid)
            {
                // Debugging: Print out ModelState errors
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"Key: {state.Key}, Error: {error.ErrorMessage}");
                    }
                }
                return View(doctor); // Redisplay the form with validation errors
            }
            if (ModelState.IsValid)
            {
                // Save the uploaded image to wwwroot/images
                if (imageFile != null && imageFile.Length > 0)
                {

                    string uploadsFolder = Path.Combine(Environment.WebRootPath, "images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(fileStream);
                    }

                    // Save the image file name (not the full path) in the doctor model
                    doctor.ImagePath = uniqueFileName;
                }
                // Simulate auto-incrementing ID
                doctor.DoctorId = _doctors.Max(d => d.DoctorId) + 1;
                _doctors.Add(doctor);
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

        // Edit doctor (GET)
        public IActionResult Edit(int id)
        {
            var doctor = _doctors.FirstOrDefault(d => d.DoctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // Edit doctor (POST)
        [HttpPost]
        public IActionResult Edit(int id, Doctor updatedDoctor, IFormFile imageFile)
        {
            var doctor = _doctors.FirstOrDefault(d => d.DoctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Save the uploaded image to wwwroot/images if a new one is uploaded
                if (imageFile != null && imageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(Environment.WebRootPath, "images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(fileStream);
                    }

                    // Update the image file name in the doctor model
                    doctor.ImagePath = uniqueFileName;
                }
                // Update doctor details
                doctor.Name = updatedDoctor.Name;
                doctor.Specialty = updatedDoctor.Specialty;
                doctor.Phone = updatedDoctor.Phone;
                doctor.YearsOfExperience = updatedDoctor.YearsOfExperience;
                doctor.Department = updatedDoctor.Department;

                return RedirectToAction(nameof(Index));
            }
            return View(updatedDoctor);
        }

        // Delete doctor (GET)
        public IActionResult Delete(int id)
        {
            var doctor = _doctors.FirstOrDefault(d => d.DoctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }
            else
            {
                _doctors.Remove(doctor);
            }
            return RedirectToAction("Index");

        }

        // Delete doctor (POST)
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var doctor = _doctors.FirstOrDefault(d => d.DoctorId == id);
            if (doctor != null)
            {
                _doctors.Remove(doctor);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
