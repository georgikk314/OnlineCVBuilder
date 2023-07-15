using Microsoft.EntityFrameworkCore;
using Online_CV_Builder.Data;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Online_CV_Builder.Services
{
    public class ResumeSharingService : IResumeSharingService
    {
        private readonly ResumeBuilderContext _dbContext;
        private readonly ITemplateDownloadService _templateDownloadService;

        public ResumeSharingService(ResumeBuilderContext dbContext, ITemplateDownloadService templateDownloadService)
        {
            _dbContext = dbContext;
            _templateDownloadService = templateDownloadService;
        }

        public async Task ShareResumeByEmail(int resumeId, string recipientEmail, string message)
        {
            // Get the resume details
            var resume = await _dbContext.Resumes.FirstOrDefaultAsync(r => r.Id == resumeId);
            if (resume == null)
            {
                throw new ArgumentException("Invalid resumeId.");
            }

            // Get the resume file path
            string filePath = _templateDownloadService.ContructionOfTemplate(resumeId);


            if (string.IsNullOrEmpty(filePath))
            {
                throw new InvalidOperationException("Resume file path is not available.");
            }

            // Read the resume file into a byte array
            byte[] fileBytes;
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var memoryStream = new MemoryStream())
                {
                    await fileStream.CopyToAsync(memoryStream);
                    fileBytes = memoryStream.ToArray();
                }
            }

            var userEmail = "gogomen2005@gmail.com";

            // Create the email message
            var emailSubject = "Resume Sharing";
            var emailBody = $"Hello,\n\nYou have received a shared resume.\n\nMessage: {message}";

            // Send the email with the resume attachment
            using (var client = new SmtpClient())
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(userEmail),
                    Subject = emailSubject,
                    Body = emailBody,
                    IsBodyHtml = false
                };

                mailMessage.To.Add(recipientEmail);

                // Attach the resume file
                using (var resumeAttachment = new MemoryStream(fileBytes))
                {
                    mailMessage.Attachments.Add(new Attachment(resumeAttachment, "resume.pdf", "application/pdf"));
                    await client.SendMailAsync(mailMessage);
                }
            }
        }

        public SmtpClient ConfigureSmtpClient(string email, string emailUsername, string emailPassword)
        {
            string emailServer = email.Split('@').Last();
            // Configure the SMTP client with the provided email credentials
            var smtpClient = new SmtpClient($"smtp.{emailServer}");
            smtpClient.Port = 465;
            smtpClient.EnableSsl = false;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(emailUsername, emailPassword);
            return smtpClient;
        }


        public async Task SendEmailAsync(SmtpClient smtpClient, string recipientEmail, string senderEmail, string attachmentFilePath)
        {
            // Create the email message
            var emailMessage = new MailMessage();
            emailMessage.From = new MailAddress(senderEmail);
            emailMessage.To.Add(new MailAddress(recipientEmail));
            emailMessage.Subject = "Sharing resume with you";
            emailMessage.Body = "Hello, I present you my resume! ";

            // Attach the resume PDF to the email
            var attachment = new Attachment(attachmentFilePath, MediaTypeNames.Application.Pdf);
            emailMessage.Attachments.Add(attachment);

            // Send the email
            await smtpClient.SendMailAsync(emailMessage);
        }
    }
}
