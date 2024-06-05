using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace VisitorSecurityClearance.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _apiKey;
        private readonly string _senderEmail;

        public EmailService(IConfiguration configuration)
        {
            _apiKey = configuration["SendGrid:ApiKey"];
            _senderEmail = configuration["SendGrid:SenderEmail"];
        }

        public async Task SendEmailAsync(string recipientEmail, string subject, string message, string pdfBase64)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress(_senderEmail, "Visitor Security Clearance System");
            var to = new EmailAddress(recipientEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);

            await client.SendEmailAsync(msg);
        }
    }
}
