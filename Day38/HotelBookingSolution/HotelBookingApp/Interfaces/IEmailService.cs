namespace HotelBookingApp.Interfaces
{
    public interface IEmailService
    {
        Task SendStatusChangeEmail(string toEmail, string subject, string message);
    }
}
