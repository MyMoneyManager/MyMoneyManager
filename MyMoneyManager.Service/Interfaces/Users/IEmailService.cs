namespace MyMoneyManager.Service.Interfaces.Users;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string message);
}
