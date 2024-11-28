using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp
{
    internal interface IValidateCustomer
    {
        void ValidateCustomerByAge(Customer customer);
    }
}
