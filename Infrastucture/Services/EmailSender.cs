using System.Net;
using System.Net.Mail;
using Infrastucture.Interfaces;

namespace Infrastucture.Services;

public class EmailSender : IEmailSender
{
    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        var smtp = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential("zaripovalisher.49@gmail.com", "yrohmrsfmpezxexv"),
            EnableSsl = true
        };

        var mail = new MailMessage("zaripovalisher.49@gmail.com", toEmail, subject, message);
        await smtp.SendMailAsync(mail);
    }
}
