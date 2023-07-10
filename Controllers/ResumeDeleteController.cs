using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Online_CV_Builder.Controllers
{
    [Route("api/resumes/{id}/delete")]
    [ApiController]
    public class ResumeDeleteController : ControllerBase
    {
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
