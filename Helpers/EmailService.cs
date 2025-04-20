using Microsoft.Extensions.Options;
using Portfolio.Settings;
using System.Net.Mail;
using System.Net;

namespace Portfolio.Helpers
{
    public class EmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public void SendEmail(string subject, string body)
        {
            var smtp = new SmtpClient
            {
                Host = _smtpSettings.Host,
                Port = _smtpSettings.Port,
                EnableSsl = _smtpSettings.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtpSettings.FromEmail, _smtpSettings.FromPassword)
            };

            using (var message = new MailMessage(_smtpSettings.FromEmail, _smtpSettings.ToEmail)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
