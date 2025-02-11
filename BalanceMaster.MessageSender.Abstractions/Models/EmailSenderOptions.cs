namespace BalanceMaster.MessageSender.Abstractions.Models;

public sealed class EmailSenderOptions
{
    public const string Key = "MailSender";

    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string SenderAddress { get; set; }
    public required string SenderName { get; set; }
    public required string SmtpServer { get; set; }
    public required int Port { get; set; }
}