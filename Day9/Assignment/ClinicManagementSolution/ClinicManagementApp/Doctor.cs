using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementApp
{
    internal class Doctor : IPerson
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Department { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }

        public List<Appointment> Appointments = new List<Appointment>();

        public void TakeInput()
        {
            Console.WriteLine("Enter Doctor Name: ");
            Name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter Phone Number: ");
            PhoneNumber = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter Department: ");
            Department = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter Username: ");
            Username = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter Password: ");
            Password = Console.ReadLine() ?? string.Empty;
        }
        public void ViewPatients(List<Patient> patients)
        {
            foreach (var patient in patients)
            {
                Console.WriteLine($"Patient: {patient.Name}, Disease: {patient.Disease}");
            }
        }
        public void ViewAppointments()
        {
            var futureAppointments = Appointments.Where(a => a.Date > DateTime.Now).ToList();
            var pastAppointments = Appointments.Where(a => a.Date <= DateTime.Now).ToList();

            Console.WriteLine("Future Appointments:");
            foreach (var app in futureAppointments)
            {
                Console.WriteLine($"Patient: {app.Patient.Name}, Time: {app.Date}");
            }

            Console.WriteLine("Past Appointments:");
            foreach (var app in pastAppointments)
            {
                Console.WriteLine($"Patient: {app.Patient.Name}, Time: {app.Date}");
            }
        }
        public static Doctor? Login(List<Doctor> doctors, string username, string password)
        {
            if (doctors == null)
            {
                throw new ArgumentNullException(nameof(doctors));
            }
            return doctors.FirstOrDefault(d => d.Username == username && d.Password == password);
        }
    }
}
