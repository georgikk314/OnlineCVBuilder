using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_CV_Builder.Data;
using Online_CV_Builder.Data.Entities;
using Online_CV_Builder.DTOs.ResumeRelatedDTOs;
using Online_CV_Builder.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Online_CV_Builder.Controllers
{
    [Route("api/resumes")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        private readonly IResumeService _resumeService;
        public ResumeController(IResumeService resumeService)
        {
            _resumeService = resumeService;
        }
        private readonly ITemplateDownloadService _templateDownloadService;
        public ResumeController(ITemplateDownloadService templateDownloadService)
        {
            _templateDownloadService = templateDownloadService;
        }
        private readonly ResumeBuilderContext _dbContext;
        public ResumeController(ResumeBuilderContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET api/resumes/view/{id} - gives a specific resume in pdf format
        [HttpGet("view/{resumeTemplateId}")]
        [Authorize]
        public PdfDocument ViewResumeAsPdf(int resumeTemplateId)
        {
            // Finds and gets the resume, template and resumetemplate entity based on an id
            var resumeTemplate = _dbContext.ResumeTemplates.FirstOrDefault(rt => rt.Id == resumeTemplateId);
            var resume = _dbContext.Resumes.FirstOrDefault(r => r.Id == resumeTemplate.ResumeId);
            var template = _dbContext.Templates.FirstOrDefault(t => t.Id == resumeTemplate.TemplateId);
            // Construct the template and get the file path
            string filePath = _templateDownloadService.ContructionOfTemplate(resumeTemplate.ResumeId.GetValueOrDefault());
            // Construct the resume as an html string
            var htmlContent = _templateDownloadService.ConstructHtmlContent(template, resume);
            // Generate a pdf file for the resume
            var pdfFile = _templateDownloadService.GeneratePdfFromHtml(htmlContent, filePath);
            // Return a success response with the file
            return pdfFile;
        }

        // POST api/resumes
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateResume([FromBody] ResumeDTO resumeDto)
        {
            var resume = await _resumeService.CreateResumeAsync(resumeDto);

            return Ok(resume);
        }

        // GET api/resumes/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetResume(int id)
        {
            var resume = await _resumeService.GetResumeAsync(id);
            return Ok(resume);
        }

        // PUT api/resumes/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateResume(int id, [FromBody] ResumeDTO resumeDto)
        {
            var updatedResume = await _resumeService.UpdateResumeAsync(id, resumeDto);
            if (updatedResume == null)
            {
                return NotFound();
            }

            return Ok(updatedResume);
        }

        // DELETE api/resumes/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteResume(int id)
        {
            var isDeleted = await _resumeService.DeleteResumeAsync(id);

            if (isDeleted)
                return NoContent();

            return NotFound();
        }

    }
}
