using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApp
{
    internal class AuthService : IAuthService
    {
        private const string ValidUsername = "ABC";
        private const string ValidPassword = "123";

        public bool ValidateCredentials(string username, string password)
        {
            return username == ValidUsername && password == ValidPassword;
        }
    }
}
