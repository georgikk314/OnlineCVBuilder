using Online_CV_Builder.Data.Entities;
using Online_CV_Builder.Data;

namespace Online_CV_Builder.Services
{
    public class ResumeService : IResumeService
    {
        private readonly ResumeBuilderContext _dbContext;

        public ResumeService(ResumeBuilderContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Resumes> CreateResumeAsync(int userId, string title)
        {
            var resume = new Resumes
            {
                UserId = userId,
                Title = title,
                CreationDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            };

            _dbContext.Resumes.Add(resume);
            await _dbContext.SaveChangesAsync();

            return resume;
        }

       // public async Task<Resumes> GetResumeAsync(int resumeId)
       // {
            
       // }
    }
}
