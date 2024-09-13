using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePromotionApp
{
    public class SalaryComparer : IComparer<Employee>
    {
        public int Compare(Employee x, Employee y)
        {
            if (x==null||y==null)
            {
                throw new ArgumentNullException("One or more null values");
            }
            return x.Salary.CompareTo(y.Salary);
        }
    }
}
