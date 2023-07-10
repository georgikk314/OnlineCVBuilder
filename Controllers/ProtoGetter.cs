using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_CV_Builder.Data.Entities;
using Online_CV_Builder.Data;

namespace Online_CV_Builder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProtoGetter : ControllerBase
    {
        private readonly ResumeBuilderContext _ResumeBuilderContext;
        public ProtoGetter(ResumeBuilderContext ResumeBuilderContext)
        {
            _ResumeBuilderContext = ResumeBuilderContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetLanguages()
        {
            if (_ResumeBuilderContext.Languages == null)
            {
                return NotFound();
            }
            // return await _ResumeBuilderContext.Languages.TolistAsync();
            return null;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetLanguage(int id)
        {
            if (_ResumeBuilderContext.Languages == null)
            {
                return NotFound();
            }
            var language = await _ResumeBuilderContext.Languages.FindAsync(id);
            if (language == null) 
            {
                return NotFound();
            }
            // return language;
            return null;
        }
    }
}
