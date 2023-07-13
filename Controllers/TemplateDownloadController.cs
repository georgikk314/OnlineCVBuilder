using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Online_CV_Builder.Controllers
{
    [Route("api/download")]
    [ApiController]
    public class TemplateDownloadController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            
        }

       
    }
}
