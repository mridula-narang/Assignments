using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorApp
{
    internal class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Department { get; set; }
        public int Age { get; set; }
        public string Level { get; set; } = string.Empty;

        public Doctor()
        {
        }
        public Doctor(int id, string name, int department, int age, string level)
        {
            Id = id;
            Name = name;
            Department = department;
            Age = age;
            Level = level;
        }
    }
}
