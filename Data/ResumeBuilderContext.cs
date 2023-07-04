using Microsoft.EntityFrameworkCore;

namespace Online_CV_Builder.Data
{
    public class ResumeBuilderContext : DbContext
    {
        public ResumeBuilderContext(DbContextOptions<ResumeBuilderContext> options) : base(options)
        {

        }
    }
}
