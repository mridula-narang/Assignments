using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementApp
{
    internal class Patient : IPerson
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Disease { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }

        public List<Appointment> Appointments = new List<Appointment>();

        public void TakeInput()
        {
            Console.WriteLine("Please enter patient name: ");
            Name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Please enter patient phone number: ");
            PhoneNumber = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Please enter patient disease: ");
            Disease = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Please enter patient username: ");
            Username = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Please enter patient password: ");
            Password = Console.ReadLine() ?? string.Empty;

        }

        public void ViewAppointments(List<Appointment> appoitments)
        {
            var futureAppointments = Appointments.Where(a => a.Date > DateTime.Now).ToList();
            var pastAppointments = Appointments.Where(a => a.Date <= DateTime.Now).ToList();

            Console.WriteLine("Future Appointments:");
            foreach (var app in futureAppointments)
            {
                Console.WriteLine($"Doctor: {app.Doctor.Name}, Date: {app.Date}");
            }

            Console.WriteLine("Past Appointments:");
            foreach (var app in pastAppointments)
            {
                Console.WriteLine($"Doctor: {app.Doctor.Name}, Date: {app.Date}");
            }
        }

        public void BookAppointment(List<Doctor> doctors, List<Appointment> allAppointments)
        {
            Console.WriteLine("Available Doctors:");
            foreach (var doc in doctors)
            {
                Console.WriteLine($"Id: {doc.Id}, Name: {doc.Name}, Department: {doc.Department}");
            }

            Console.WriteLine("Enter doctor id to book appointment: ");
            string? input = Console.ReadLine();
            if (!int.TryParse(input, out int doctorId))
            {
                throw new FormatException("Invalid input. Please enter a valid doctor ID.");
            }

            // Find the selected doctor
            var doctor = doctors.FirstOrDefault(doctor => doctor.Id == doctorId);
            Console.WriteLine("Enter appointment date and time (yyyy-mm-dd HH:mm): ");
            string? dateInput = Console.ReadLine();
            if (string.IsNullOrEmpty(dateInput))
            {
                throw new ArgumentNullException(nameof(dateInput), "Date input cannot be null or empty.");
            }
            DateTime appointmentDate = DateTime.Parse(dateInput);
            if (doctor == null)
            {
                Console.WriteLine("Invalid Doctor Id.");
                return;
            }

            // Check if any existing appointment is within 30 minutes of the requested time
            bool isConflict = allAppointments.Any(a => a.Doctor.Id == doctorId
                            && Math.Abs((a.Date - appointmentDate).TotalMinutes) < 30);

            if (isConflict)
            {
                Console.WriteLine("This time slot is too close to another appointment. Please choose a time that is at least 30 minutes apart.");
                return;
            }

            // Create a new appointment
            var appointment = new Appointment
            {
                Doctor = doctor,
                Patient = this,
                Date = appointmentDate
            };

            // Add the appointment to both the patient's and doctor's appointment lists
            this.Appointments.Add(appointment);
            doctor.Appointments.Add(appointment);  // Add to doctor's appointments

            // Add the appointment to the global list
            allAppointments.Add(appointment);

            Console.WriteLine("Appointment booked successfully.");
        }


        public static Patient? Login(List<Patient> patients, string username, string password)
        {
            if (patients == null)
            {
                throw new ArgumentNullException(nameof(patients));
            }
            return patients.FirstOrDefault(p => p.Username == username && p.Password == password);
        }
    }
}
