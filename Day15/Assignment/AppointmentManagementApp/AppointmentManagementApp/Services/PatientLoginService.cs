using AppointmentManagementApp.Interfaces;
using AppointmentManagementApp.Models;

namespace AppointmentManagementApp.Services
{
    public class PatientLoginService : ILoginService
    {
        private IRepository<string, Patient> _patientRepository;

        public PatientLoginService(IRepository<string, Patient> patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            var myPatient = _patientRepository.Get(username);
            if (myPatient == null || !ComparePassword(myPatient.Password, oldPassword)) throw new Exception("Unable to validate user");
            if (!ComparePassword(newPassword, oldPassword))
            {
                myPatient.Password = newPassword;
                return true;
            }
            throw new Exception("Old and new password cannot be the same"); ;
        }
        public bool Login(string username, string password)
        {
            var myPatient = _patientRepository.Get(username);
            return ComparePassword(myPatient.Password, password);
        }
        private bool ComparePassword(string oldPassword, string newPassword)
        {
            return oldPassword == newPassword;        
        }

        public Patient GetPatientByUsername(string username)
        {
            return _patientRepository.GetAll().FirstOrDefault(p => p.Username == username);
        }
    }
}
