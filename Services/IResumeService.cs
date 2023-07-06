using Online_CV_Builder.Data.Entities;

namespace Online_CV_Builder.Services
{
    public interface IResumeService
    {
        Task<Resumes> CreateResumeAsync(int userId, string title);
       // Task<Resumes> GetResumeAsync(int resumeId);
    }
}
