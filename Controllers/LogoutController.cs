using Microsoft.AspNetCore.Mvc;

namespace Online_CV_Builder.Controllers
{
    [Route("api/logout")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
        [Route("api/resumedelete/{id}")]
        [ApiController]
        public class ResumeDeleteController : ControllerBase
        {
            [HttpDelete("{id}")]
            public void DeleteResume(int id)
            {

            }
        }
    }
}