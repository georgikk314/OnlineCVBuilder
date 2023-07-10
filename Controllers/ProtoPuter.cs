using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_CV_Builder.Data.Entities;
using Online_CV_Builder.Data;

namespace Online_CV_Builder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProtoPuter : ControllerBase
    {
        private readonly ResumeBuilderContext _ResumeBuilderContext;
        public ProtoPuter(ResumeBuilderContext ResumeBuilderContext)
        {
            _ResumeBuilderContext = ResumeBuilderContext;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutLangugage(int id, Languages language2) 
        {
            if (id == language2.Id) 
            {
                return BadRequest();
            }
            _ResumeBuilderContext.Entry(language2).State = EntityState.Modified;
            try 
            {
                await _ResumeBuilderContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) 
            {
                if (!LanguageExists(id)) 
                {
                    return NotFound();
                }
                else 
                {
                    throw;
                }
            }
            return NoContent();
        }
        private bool LanguageExists(int id) 
        {
            return (_ResumeBuilderContext.Languages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
