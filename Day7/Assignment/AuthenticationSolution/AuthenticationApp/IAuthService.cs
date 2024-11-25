using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApp
{
    public interface IAuthService
    {
        bool ValidateCredentials(string username, string password);
    }
}
