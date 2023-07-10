using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_CV_Builder.Data;
using Online_CV_Builder.Controllers;
using Online_CV_Builder.Data.Entities;
using Online_CV_Builder.DTOs;
using Online_CV_Builder.DTOs.ResumeRelatedDTOs;

namespace Online_CV_Builder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProtoPoster : ControllerBase
    {
        private readonly ResumeBuilderContext _ResumeBuilderContext;
        public ProtoPoster(ResumeBuilderContext ResumeBuilderContext)
        {
            _ResumeBuilderContext = ResumeBuilderContext;
        }
        [HttpPost]
        public async Task<IActionResult> PostLanguages([FromBody] LanguageDTO languageDTO)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            var languages = await _ResumeBuilderContext.Languages.ToListAsync();
            _ResumeBuilderContext.Languages.AddRange(languages);
            await _ResumeBuilderContext.SaveChangesAsync();
            return CreatedAtAction(nameof(ProtoGetter.GetLanguage), new { id = languageDTO.LanguageId}, languages);
        }
    }
}
