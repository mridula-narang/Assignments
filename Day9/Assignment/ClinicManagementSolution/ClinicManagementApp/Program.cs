namespace ClinicManagementApp
{
    internal class Program
    {
        private List<Doctor> doctors = new List<Doctor>();
        private List<Patient> patients = new List<Patient>();
        private List<Appointment> appointments = new List<Appointment>();

        public void PrintMenu()
        {
            while (true)
            {
                Console.WriteLine("Welcome to our clinic!! Please select one of the available services.");
                Console.WriteLine("1. Register Patient");
                Console.WriteLine("2. Register Doctor");
                Console.WriteLine("3. Login");
                Console.WriteLine("4. Exit");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterPatient();
                        break;
                    case "2":
                        RegisterDoctor();
                        break;
                    case "3":
                        Login();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Please enter a valid choice.");
                        break;
                }
            }
        }
        public void RegisterPatient()
        {
            Patient newPatient = new Patient();
            newPatient.TakeInput();
            newPatient.Id = patients.Count + 1;
            patients.Add(newPatient);
        }
        public void RegisterDoctor()
        {
            Doctor newDoctor = new Doctor();
            newDoctor.TakeInput();
            newDoctor.Id = doctors.Count + 1;
            doctors.Add(newDoctor);
        }
        public void Login()
        {
            Console.WriteLine("1. Patient Login");
            Console.WriteLine("2. Doctor Login");

            var loginChoice = Console.ReadLine();
            switch (loginChoice)
            {
                case "1":
                    PatientLogin();
                    break;
                case "2":
                    DoctorLogin();
                    break;
                default:
                    Console.WriteLine("Please enter a valid choice.");
                    break;
            }
        }
        public void PatientLogin()
        {
            Console.Write("Enter Username: ");
            string? username = Console.ReadLine();
            Console.Write("Enter Password: ");
            string? password = Console.ReadLine();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Username or password cannot be empty.");
                return;
            }

            var patient = Patient.Login(patients, username, password);
            if (patient != null)
            {
                HandlePatient(patient);
            }
            else
            {
                Console.WriteLine("Invalid username or password.");
                PatientLogin();
            }
        }
        public void DoctorLogin()
        {
            Console.Write("Enter Username: ");
            string? username = Console.ReadLine();
            Console.Write("Enter Password: ");
            string? password = Console.ReadLine();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Username and password cannot be empty.");
                return;
            }

            var doctor = Doctor.Login(doctors, username, password);
            if (doctor != null)
            {
                HandleDoctor(doctor);
            }
            else
            {
                Console.WriteLine("Invalid username or password.");
                DoctorLogin();
            }
        }
        public void HandlePatient(Patient patient)
        {
            while (true)
            {
                Console.WriteLine($"Welcome dear {patient.Name}. How can we help you today?");
                Console.WriteLine("1. Book Appointment");
                Console.WriteLine("2. View Appointments");
                Console.WriteLine("3. Logout");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        patient.BookAppointment(doctors, appointments);
                        break;
                    case "2":
                        patient.ViewAppointments(appointments);
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid Choice.");
                        break;
                }
            }
        }
        public void HandleDoctor(Doctor doctor)
        {
            while (true)
            {
                Console.WriteLine($"Welcome Dr. {doctor.Name}. What would you like to do today?");
                Console.WriteLine("1. View Patients");
                Console.WriteLine("2. View Appointments");
                Console.WriteLine("3. Logout");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        doctor.ViewPatients(patients);
                        break;
                    case "2":
                        doctor.ViewAppointments();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid Choice.");
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            try
            {
                Program program = new Program();
                program.PrintMenu();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("User does not exist! Please register as a doctor or patient.");
            }
            catch (FormatException e)
            {
                Console.WriteLine("Invalid input. Please enter a valid doctor ID.");
            }
        }
      
    }
}
