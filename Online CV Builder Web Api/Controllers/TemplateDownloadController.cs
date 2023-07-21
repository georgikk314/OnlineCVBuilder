using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_CV_Builder.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Online_CV_Builder.Controllers
{
    [Route("api/download")]
    [ApiController]
    public class TemplateDownloadController : ControllerBase
    {
        private readonly ITemplateDownloadService _templateDownloadService;

        public TemplateDownloadController(ITemplateDownloadService templateDownloadService)
        {
            _templateDownloadService = templateDownloadService;
        }

        [HttpGet("{resumeId}")]
        //[Authorize]
        public async Task<IActionResult> DownloadTemplate(int resumeId)
        {
            try
            {
                // Construct the template and get the file path
                string filePath = _templateDownloadService.ContructionOfTemplate(resumeId);

                // Download the file
                await _templateDownloadService.PDFDownloader(filePath);

                // Delete the file in the app storage
                _templateDownloadService.DeletePdfTemplate(filePath);

                // Return a success response
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (IOException ex)
            {
                return StatusCode(500, "An error occurred while generating the template.");
            }
        }
    }
}
