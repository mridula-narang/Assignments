using System.Drawing;

namespace DoctorApp
{
    internal class Program
    {
        Hospital hospital;
        public void PrintMenu()
        {
            Console.WriteLine("Welcome to the hospital!");
            Console.WriteLine("1. Add Doctor");
            Console.WriteLine("2. Assign Department");
            Console.WriteLine("3. Display Doctor");
            Console.WriteLine("4. Exit");
        }

        public void CreateDoctor()
        {
            Console.WriteLine("Please enter the number of doctors you would like to add:");
            int size = Convert.ToInt32(Console.ReadLine());
            hospital = new Hospital(size);
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine($"\nPlease enter details for Doctor {i + 1}:");

                Console.WriteLine("Enter doctor's id:");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter doctor's name:");
                string name = Console.ReadLine();

                Console.WriteLine("Enter doctor's department id:");
                int department = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter doctor's age:");
                int age = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter doctor's level:");
                string level = Console.ReadLine();

                Doctor doctor = new Doctor(id, name, department, age, level);
                hospital.AddDoctor(doctor);
            }
        }

        public void DepartmentAssignment()
        {
            int size = hospital.Doctors.Length;
            for (int i = 0; i < size; i++)
            {
                Console.Write($"\nEnter the Department ID to assign to Doctor {hospital.Doctors[i].Name}: ");
                int departmentId = Convert.ToInt32(Console.ReadLine());

                hospital.AssignDepartment(hospital.Doctors[i], departmentId);
            }
        }

        public void ViewDoctors()
        {
            hospital.DisplayDoctor();
        }

        public void MainInteraction()
        {
            int choice;
            do
            {
                PrintMenu();
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        CreateDoctor();
                        break;
                    case 2:
                        DepartmentAssignment();
                        break;
                    case 3:
                        ViewDoctors();
                        break;
                    case 4:
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            } while (choice != 4);
        }

        static void Main(string[] args)
        {
            new Program().MainInteraction();
        }
    }
}
