﻿using HospitalManagementEFApp.Contexts;
using HospitalManagementEFApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementEFApp
{
    internal class Program
    {
        AppointmentContext context = new AppointmentContext();
        //initial method to create patients as after db creation patient table will be empty.
        Patient CreateAndPopulatePatient()
        {
            Patient patient = new Patient();
            Console.WriteLine("Please enter patient name: ");
            patient.PatientName = Console.ReadLine();
            Console.WriteLine("Please enter patient phone: ");
            patient.Phone = Console.ReadLine();
            Console.WriteLine("Please enter the disease the patient is suffering from: ");
            patient.Disease = Console.ReadLine();
            return patient;
        }
        void InsertPatient()
        {
            Patient patient = CreateAndPopulatePatient();
            try
            {
                context.Patients.Add(patient);
                context.SaveChanges();
                Console.WriteLine("Patient details added successfully!");
                Console.WriteLine($"Your Patient ID is: {patient.PatientId}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Could not add patient details.");
            }
        }
        //initial method to create patients as after db creation patient table will be empty.
        Doctor CreateAndPopulateDoctor()
        {
            Doctor doctor = new Doctor();
            Console.WriteLine("Please enter doctor name: ");
            doctor.DoctorName = Console.ReadLine();
            Console.WriteLine("Please enter doctor department: ");
            doctor.Department = Console.ReadLine();
            Console.WriteLine("Please enter doctor phone: ");
            doctor.Phone = Console.ReadLine();
            return doctor;
        }
        void InsertDoctor()
        {
            Doctor doctor = CreateAndPopulateDoctor();
            try
            {
                context.Doctors.Add(doctor);
                context.SaveChanges();
                Console.WriteLine("Doctor details added successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Could not add doctor details.");
            }
        }
        void PrintMenu() 
        {
            while (true)
            {
                Console.WriteLine("Welcome to hospital management system! Please select a service.");
                Console.WriteLine("1.List doctors");
                Console.WriteLine("2.Book Appointment");
                Console.WriteLine("3.Exit");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        ListDoctors();
                        break;
                    case 2:
                        BookAppointment();
                        break;
                    case 3:
                        return;
                    default:
                        break;
                }


            }
        }

        private void BookAppointment()
        {
            try
            {
                Console.WriteLine("Enter patient id: ");
                int patientId = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter doctor id: ");
                int doctorId = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter appointment date and time (yyyy-mm-dd hh:mm): ");
                DateTime appointmentDate = DateTime.Parse(Console.ReadLine());
                bool appointmentExists = context.Appointments.Any(a => a.DoctorId == doctorId && a.AppointmentDate == appointmentDate);

                if (appointmentExists)
                {
                    Console.WriteLine("Sorry,the doctor already has an appointment at this time.\n Please select another time");
                    return;
                }
                var appointment = new Appointment
                {
                    PatientId = patientId,
                    DoctorId = doctorId,
                    AppointmentDate = appointmentDate
                };
                context.Appointments.Add(appointment);
                context.SaveChanges();
                Console.WriteLine("Appointment booked successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Could not book appointment");
            }
        }

        private void ListDoctors()
        {
            var doctors = context.Doctors.ToList();
            foreach (var doctor in doctors)
            {
                Console.WriteLine($"Doctor id: {doctor.DoctorId}\nDoctor Name: {doctor.DoctorName}\nDepartment: {doctor.Department}");
                Console.WriteLine("===========================================================================");
            }
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            //program.InsertPatient();
            //program.InsertDoctor();
            program.PrintMenu();
        }
    }
}
