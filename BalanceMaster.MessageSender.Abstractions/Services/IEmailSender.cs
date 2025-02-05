namespace BalanceMaster.MessageSender.Abstractions.Services;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string message);
}