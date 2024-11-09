using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DoctorApp
{
    internal class Hospital : IDoctor
    {
        private Doctor[] doctors;
        private int count = 0;
        public Hospital(int size)
        {
            doctors = new Doctor[size];
        }
        public void AddDoctor(Doctor doctor)
        {
            try
            {
                if (doctor == null)
                {
                    throw new ArgumentNullException(nameof(doctor), "Doctor name cannot be null.");
                }
                if (count>=doctors.Length)
                {
                    throw new InvalidOperationException("All positions in the hospital are full. Cannot recruit more doctors.");
                }
                doctors[count] = doctor;
                count++;
                Console.WriteLine("Doctor successfully added");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Name cannot be null");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("No empty positions");
            }
        }
        public void AssignDepartment(Doctor doctor, int departmentId)
        {
            try
            {
                if (doctor == null)
                {
                    throw new ArgumentNullException(nameof(doctor), "Doctor name cannot be null.");
                }
                if (doctor.Department <= 0)
                {
                    throw new InvalidOperationException("Invalid department id. Department id must be greater than 0");
                }
                doctor.Department = departmentId;
                Console.WriteLine($"Doctor {doctor.Name} is assigned to department {doctor.Department}");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Name cannot be null");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Department cannot be null");
            }
        }

        public void DisplayDoctor()
        {
            try
            {
                if (count == 0)
                {
                    Console.WriteLine("No doctors to display.");
                    return;
                }

                for (int i = 0; i < count; i++)
                {
                    Doctor doctor = doctors[i];
                    Console.WriteLine($"Doctor ID: {doctor.Id}, Name: {doctor.Name}, Department ID: {doctor.Department}, Age: {doctor.Age}, Level: {doctor.Level}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying doctors: {ex.Message}");
            }
        }
        public Doctor[] Doctors
        {
            get { return doctors; }
        }
    }
}
