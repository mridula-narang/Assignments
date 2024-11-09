using AppointmentManagementApp.Exceptions;
using AppointmentManagementApp.Interfaces;
using AppointmentManagementApp.Models;

namespace AppointmentManagementApp.Repositories
{
    public class DoctorRepository : IRepository<string, Doctor>
    {
        Dictionary<string, Doctor> _doctors = new Dictionary<string, Doctor>
        {
            {"hermione", new Doctor {DoctorId = 1, Name = "Hermione Granger",Department = "General OPD",Username = "hermione", Password = "1234"} },
            {"luna", new Doctor {DoctorId = 2, Name = "Luna Lovegood",Department = "Opthalmology", Username = "luna", Password = "1234"} },
        };
        public Doctor Add(Doctor item)
        {
            if (!_doctors.ContainsKey(item.Username))
            {
                _doctors.Add(item.Username, item);
            }
            throw new DuplicateDoctorException();
        }

        public Doctor Delete(string key)
        {
            var doctor = Get(key);
            if (doctor != null)
            {
                _doctors.Remove(key);
                return doctor;
            }
            throw new KeyNotFoundException("Doctor not found.");
        }

        public Doctor Get(string key)
        {
            var doctor = _doctors[key];
            if (doctor == null)
            {
                throw new DoctorNotFoundException();
            }
            return doctor;
        }

        public List<Doctor> GetAll()
        {
            if (_doctors.Count == 0)
            {
                throw new NoDoctorsException();
            }
            return _doctors.Values.ToList();
        }

        public Doctor Update(Doctor Item)
        {
            var doctor = Get(Item.Username);
            if (doctor != null)
            {
                _doctors[Item.Username] = Item;
            }
            return doctor;
        }
    }
}
