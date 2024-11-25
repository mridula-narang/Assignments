using AppointmentManagementApp.Interfaces;
using AppointmentManagementApp.Models;
using AppointmentManagementApp.Exceptions;
namespace AppointmentManagementApp.Repositories
{
    public class PatientRepository : IRepository<string, Patient>
    {
        Dictionary<string, Patient> _patients = new Dictionary<string, Patient>
        {
            {"harry", new Patient {PatientId = 1, Name = "Harry", Username = "harry", Password = "1234", Disease = "Diabetes" } },
            {"ron", new Patient {PatientId = 2, Name = "Ron", Username = "ron", Password = "1234", Disease = "Glaucoma"} },
        };
        public Patient Add(Patient item)
        {
            if(!_patients.ContainsKey(item.Username))
            {
                _patients.Add(item.Username, item);
            }
            throw new DuplicatePatientException();
        }

        public Patient Delete(string key)
        {
            var patient = Get(key);
            if (patient != null)
            {
                _patients.Remove(key);
                return patient;
            }
            throw new KeyNotFoundException("Patient not found.");
        }

        public Patient Get(string key)
        {
            var patient = _patients[key];
            if (patient == null)
            {
                throw new PatientNotFoundException();
            }
            return patient;
        }

        public List<Patient> GetAll()
        {
            if (_patients.Count == 0)
            {
                throw new NoPatientsException();
            }
            return _patients.Values.ToList();
        }

        public Patient Update(Patient Item)
        {
            var patient = Get(Item.Username);
            if (patient != null)
            {
                _patients[Item.Username] = Item;
            }
            return patient;
        }
    }
}
