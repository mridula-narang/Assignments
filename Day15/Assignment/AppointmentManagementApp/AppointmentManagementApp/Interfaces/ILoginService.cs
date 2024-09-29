using AppointmentManagementApp.Models;

namespace AppointmentManagementApp.Interfaces
{
    public interface ILoginService
    {
        bool Login(string username, string password);

        bool ChangePassword(string username, string oldPassword, string newPassword);

        Patient GetPatientByUsername(string username);
    }
}
