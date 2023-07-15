using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public ResumeSharingController(IResumeSharingService resumeSharingService, ITemplateDownloadService templateDownloadService)
        {
            _resumeSharingService = resumeSharingService;
            _templateDownloadService = templateDownloadService;
        }

        // POST api/sharing/{id}
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> ShareResume([FromBody] ResumeSharingDTO sharingDto)
        {
            try
            {
                string email = "gogomen05@abv.bg";
                var resumeId = sharingDto.ResumeId;
                var recipientEmail = sharingDto.RecipientEmail;
                var message = sharingDto.Message;
                var smtpClient = _resumeSharingService.ConfigureSmtpClient(email, sharingDto.EmailUsername, sharingDto.EmailPassword);
                var attachmentFile = _templateDownloadService.ContructionOfTemplate(resumeId);
                // Share the resume via email
                await _resumeSharingService.SendEmailAsync(smtpClient, sharingDto.RecipientEmail, email, attachmentFile);
                _templateDownloadService.DeletePdfTemplate(attachmentFile);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while sharing the resume: {ex.Message}");
            }
        }

    }
}
