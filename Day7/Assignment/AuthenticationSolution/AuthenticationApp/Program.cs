namespace AuthenticationApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IAuthService authService = new AuthService();
            AuthManager authManager = new AuthManager(authService);

            authManager.Login();
        }
    }
}
