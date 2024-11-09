namespace FinTrack.Interfaces;

public interface IMailService
{
    Task SendEmail(string toEmail, string subject, string body);
}
