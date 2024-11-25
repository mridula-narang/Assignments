using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorApp
{
    internal interface IDoctor
    {
        void AddDoctor(Doctor doctor);
        void AssignDepartment(Doctor doctor, int departmentId); //assign/ change the department of the doctor
        void DisplayDoctor();

    }
}
