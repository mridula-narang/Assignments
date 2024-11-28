using System.ComponentModel.DataAnnotations;

namespace ClinicManagementApp.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Specialty is required")]
        public string Specialty { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Years of experience is required")]
        [Range(0, 100, ErrorMessage = "Years of experience must be between 0 and 100")]
        public int YearsOfExperience { get; set; }

        // ImagePath can be optional, as not all doctors may have an image
        public string ImagePath { get; set; } = "doctor1.jpg";
    }
}
