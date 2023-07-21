using System.Net.Mail;

namespace Online_CV_Builder.Services
{
    public interface IResumeSharingService
    {
        public Task ShareResumeByEmail(int resumeId, string recipientEmail, string message);
        public Task SendEmailAsync(SmtpClient smtpClient, string recipientEmail, string senderEmail, string attachmentFilePath);
        public SmtpClient ConfigureSmtpClient(string email, string emailUsername, string emailPassword);
    }
}
