using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SystemSupportingMSE.Core;
using SystemSupportingMSE.Helpers;

namespace SystemSupportingMSE.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            this.emailSettings = emailSettings.Value;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(emailSettings.Host, emailSettings.Port)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(emailSettings.Email, emailSettings.Password),
                EnableSsl = emailSettings.EnableSSL
            };
            var mailMessage = new MailMessage(emailSettings.Email, email)
            {
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            return client.SendMailAsync(mailMessage);
        }
    }
}