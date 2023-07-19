using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_CV_Builder.Data;
using Online_CV_Builder.Data.Entities;

namespace Online_CV_Builder.Controllers
{
    [Route("api/templates")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        private readonly ResumeBuilderContext _dbContext;

        public TemplatesController(ResumeBuilderContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Templates
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Templates>>> GetTemplates()
        {
            var templatesData = await _dbContext.Templates.ToListAsync();

            return templatesData;
        }
    }
}
