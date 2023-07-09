using Online_CV_Builder.Data.Entities;
using Online_CV_Builder.DTOs.ResumeRelatedDTOs;

namespace Online_CV_Builder.Services
{
    public interface IResumeService
    {
        Task<Resumes> CreateResumeAsync(ResumeDTO resumeDTO);
        Task<ResumeDTO> GetResumeAsync(int resumeId);
        Task<bool> DeleteResumeAsync(int resumeId);
    }
}
