﻿using Microsoft.AspNetCore.Mvc;
using Online_CV_Builder.Data.Entities;
using Online_CV_Builder.DTOs;
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

        // POST api/resumes
        [HttpPost]
        public async Task<IActionResult> CreateResume([FromBody] ResumeDTO resumeDto)
        {
            var resume = await _resumeService.CreateResumeAsync(resumeDto.UserId, resumeDto.Title);

            return Ok(resume);
        }

        // GET api/resumes/{id}
        [HttpGet("{id}")]
        

        // PUT api/resumes/{id}
        [HttpPut("{id}")]
        

        // DELETE api/resumes/{id}
        [HttpDelete("{id}")]
        
    }
}