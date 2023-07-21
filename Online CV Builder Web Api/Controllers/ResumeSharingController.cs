using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_CV_Builder.Data;
using Online_CV_Builder.DTOs.SharingRelatedDTOs;
using Online_CV_Builder.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Online_CV_Builder.Controllers
{
    [Route("api/sharing")]
    [ApiController]

    public class ResumeSharingController : ControllerBase
    {
        private readonly IResumeSharingService _resumeSharingService;
        private readonly ITemplateDownloadService _templateDownloadService;
        private readonly ResumeBuilderContext _dbContext;

        public ResumeSharingController(IResumeSharingService resumeSharingService, ITemplateDownloadService templateDownloadService, ResumeBuilderContext dbContext)
        {
            _resumeSharingService = resumeSharingService;
            _templateDownloadService = templateDownloadService;
            _dbContext = dbContext;
        }

        // POST api/sharing
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> ShareResume([FromBody] ResumeSharingDTO sharingDto)
        {
            try
            {
                string email = "online.cv.builder23@gmail.com";
                string password = "tuwmnijkpixhuiln";
                var resumeTitle = sharingDto.ResumeTitle;
                var recipientEmail = sharingDto.RecipientEmail;
                var message = sharingDto.Message;
                var smtpClient = _resumeSharingService.ConfigureSmtpClient(email, email, password);

                var resumeId = (int)_dbContext.Resumes.FirstOrDefault(r => r.Title == resumeTitle).Id;
                var attachmentFile = _templateDownloadService.ContructionOfTemplate(resumeId);
                // Share the resume via email
                await _resumeSharingService.SendEmailAsync(smtpClient, sharingDto.RecipientEmail, email, attachmentFile);
                //_templateDownloadService.DeletePdfTemplate(attachmentFile);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while sharing the resume: {ex.Message}");
            }
        }

    }
}
