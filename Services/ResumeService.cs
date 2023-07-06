using Online_CV_Builder.Data.Entities;
using Online_CV_Builder.Data;
using Online_CV_Builder.DTOs;
using Online_CV_Builder.DTOs.ResumeRelatedDTOs;
using System.ComponentModel.DataAnnotations;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace Online_CV_Builder.Services
{
    public class ResumeService : IResumeService
    {
        private readonly ResumeBuilderContext _dbContext;

        public ResumeService(ResumeBuilderContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Resumes> CreateResumeAsync(ResumeDTO resumeDto)
        {
            var validationContext = new ValidationContext(resumeDto, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(resumeDto, validationContext, validationResults, validateAllProperties: true);

            var resume = new Resumes
            {
                Id = resumeDto.ResumeId,
                UserId = resumeDto.UserId,
                Title = resumeDto.Title,
                CreationDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            };

            _dbContext.Resumes.Add(resume);
            await _dbContext.SaveChangesAsync();

            // Create and associate PersonalInfo
            if (resumeDto.PersonalInfo != null)
            {
                var personalInfo = new PersonalInfo
                {
                    ResumeId = resume.Id,
                    FullName = resumeDto.PersonalInfo.FullName,
                    Address = resumeDto.PersonalInfo.Address,
                    PhoneNumber = resumeDto.PersonalInfo.PhoneNumber,
                    Email = resumeDto.PersonalInfo.Email
                };
                _dbContext.PersonalInfos.Add(personalInfo);
            }

            // Create and associate Educations
            if (resumeDto.Educations != null)
            {
                foreach (var educationDto in resumeDto.Educations)
                {
                    var education = new Education
                    {
                        ResumeId = resume.Id,
                        InstituteName = educationDto.InstituteName,
                        Degree = educationDto.Degree,
                        FieldOfStudy = educationDto.FieldOfStudy,
                        StartDate = educationDto.StartDate,
                        EndDate = educationDto.EndDate
                    };
                    _dbContext.Education.Add(education);
                }
            }

            // Create and associate WorkExperiences
            if (resumeDto.WorkExperiences != null)
            {
                foreach (var experienceDto in resumeDto.WorkExperiences)
                {
                    var workExperience = new WorkExperience
                    {
                        ResumeId = resume.Id,
                        CompanyName = experienceDto.CompanyName,
                        Position = experienceDto.Position,
                        StartDate = experienceDto.StartDate,
                        EndDate = experienceDto.EndDate,
                        Description = experienceDto.Description
                    };
                    _dbContext.WorkExperiences.Add(workExperience);
                }
            }

            // Create and associate Skills
            if (resumeDto.Skills != null)
            {
                foreach (var skillDto in resumeDto.Skills)
                {
                    var skill = new Skills
                    {
                        SkillName = skillDto.SkillName
                    };
                    _dbContext.Skills.Add(skill);

                    var resumeSkill = new ResumeSkills
                    {
                        ResumeId = resume.Id,
                        Skill = skill
                    };
                    _dbContext.ResumeSkills.Add(resumeSkill);
                }
            }

            // Create and associate Languages
            if (resumeDto.Languages != null)
            {
                foreach (var languageDto in resumeDto.Languages)
                {
                    var language = new Languages
                    {
                        LanguageName = languageDto.LanguageName,
                        ProficiencyLevel = languageDto.ProficiencyLevel
                    };
                    _dbContext.Languages.Add(language);

                    var resumeLanguage = new ResumeLanguages
                    {
                        ResumeId = resume.Id,
                        Language = language
                    };
                    _dbContext.ResumeLanguages.Add(resumeLanguage);
                }
            }

            // Create and associate Locations
            if (resumeDto.Locations != null)
            {
                foreach (var locationDto in resumeDto.Locations)
                {
                    var location = new Locations
                    {
                        City = locationDto.City,
                        State = locationDto.State,
                        Country = locationDto.Country
                    };
                    _dbContext.Locations.Add(location);

                    var resumeLocation = new ResumeLocations
                    {
                        ResumeId = resume.Id,
                        Location = location
                    };
                    _dbContext.ResumeLocations.Add(resumeLocation);
                }
            }

            // Create and associate Certificates
            if (resumeDto.Certificates != null)
            {
                foreach (var certificateDto in resumeDto.Certificates)
                {
                    var certificate = new Certificates
                    {
                        ResumeId = resume.Id,
                        CertificateName = certificateDto.CertificateName,
                        IssuingOrganization = certificateDto.IssuingOrganization,
                        IssueDate = certificateDto.IssueDate
                    };
                    _dbContext.Certificates.Add(certificate);
                }
            }

            // Save changes after adding entities
            await _dbContext.SaveChangesAsync();

            return resume;
        }

       // public async Task<Resumes> GetResumeAsync(int resumeId)
       // {
            
       // }
    }
}
