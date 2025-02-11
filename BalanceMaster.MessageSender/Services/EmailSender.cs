using BalanceMaster.MessageSender.Abstractions.Models;
using BalanceMaster.MessageSender.Abstractions.Services;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BalanceMaster.MessageSender.Services;

internal class EmailSender : IEmailSender
{
    private readonly EmailSenderOptions _options;

    public EmailSender(IOptions<EmailSenderOptions> options)
    {
        _options = options.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        // Create the email message
        var emailMessage = new MimeMessage();

        // From address
        emailMessage.From.Add(new MailboxAddress(
            _options.SenderName,
            _options.SenderAddress));

        // To address
        emailMessage.To.Add(MailboxAddress.Parse(email));

        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart("plain")
        {
            Text = message
        };

        // Send email using Gmail's SMTP server
        using var client = new MailKit.Net.Smtp.SmtpClient();

        // For development only. In production, you should properly handle certificate validation.
        client.ServerCertificateValidationCallback = (s, c, h, e) => true;

        var host = _options.SmtpServer;
        var port = _options.Port;
        var username = _options.UserName;
        var password = _options.Password;

        // Connect to the SMTP server using STARTTLS on port 587
        await client.ConnectAsync(host, port, SecureSocketOptions.StartTls);

        // Authenticate using your Gmail credentials

        await client.AuthenticateAsync(username, password);

        // Send the email
        await client.SendAsync(emailMessage);

        // Disconnect from the SMTP server
        await client.DisconnectAsync(true);
    }
}