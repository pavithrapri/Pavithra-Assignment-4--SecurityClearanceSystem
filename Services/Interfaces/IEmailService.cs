using System.Threading.Tasks;

namespace VisitorSecurityClearance.Services
{
    // Interface
    public interface IEmailService
    {
        Task SendEmailAsync(string recipientEmail, string subject, string message, string attachmentBase64);
    }

}
