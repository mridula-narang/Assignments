using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApp
{
    public class AuthManager
    {
        private readonly IAuthService _authService;
        private const int MaxAttempts = 3;

        public AuthManager(IAuthService authService)
        {
            _authService = authService;
        }

        public void Login()
        {
            int attempts = 0;
            while (attempts < MaxAttempts)
            {
                Console.Write("Enter username: ");
                string username = Console.ReadLine();

                Console.Write("Enter password: ");
                string password = Console.ReadLine();

                try
                {
                    if (_authService.ValidateCredentials(username, password))
                    {
                        Console.WriteLine("Login successful!");
                        return;
                    }
                    else
                    {
                        throw new InvalidCredentialException("Username or password is incorrect.");
                    }
                }
                catch (InvalidCredentialException ex)
                {
                    Console.WriteLine(ex.Message);
                    attempts++;
                    if (attempts == MaxAttempts)
                    {
                        Console.WriteLine("You have exceeded the number of attempts.");
                    }
                }
            }
        }
    }
}
