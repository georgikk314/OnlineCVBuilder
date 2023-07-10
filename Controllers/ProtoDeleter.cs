using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_CV_Builder.Data;
using Online_CV_Builder.Data.Entities;

namespace Online_CV_Builder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProtoDeleter : ControllerBase
    {
        private readonly ResumeBuilderContext _ResumeBuilderContext;
        public ProtoDeleter(ResumeBuilderContext ResumeBuilderContext)
        {
            _ResumeBuilderContext = ResumeBuilderContext;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLanguage(int id) 
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
            _ResumeBuilderContext.Languages.Remove(language);
            await _ResumeBuilderContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
