using Online_CV_Builder.Data.Entities;
using Online_CV_Builder.Data;
using Online_CV_Builder.DTOs;
using Online_CV_Builder.DTOs.ResumeRelatedDTOs;
using System.ComponentModel.DataAnnotations;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using AutoMapper;

namespace Online_CV_Builder.Services
{
    public class ResumeService : IResumeService
    {
        private readonly IMapper _mapper;
        private readonly ResumeBuilderContext _dbContext;

        public ResumeService(ResumeBuilderContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Resumes> CreateResumeAsync(ResumeDTO resumeDto)
        {
            var validationContext = new ValidationContext(resumeDto, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(resumeDto, validationContext, validationResults, validateAllProperties: true);

            //var resume = _mapper.Map<Resumes>(resumeDto);
            var resume = new Resumes()
            {
                Title = resumeDto.Title
            };

            resume.CreationDate = DateTime.Now;
            resume.LastModifiedDate = DateTime.Now;

            _dbContext.Resumes.Add(resume);
            await _dbContext.SaveChangesAsync();

            // Create and associate PersonalInfo
            if (resumeDto.PersonalInfo != null)
            {
                //var personalInfo = _mapper.Map<PersonalInfo>(resumeDto.PersonalInfo);
                var personalInfo = new PersonalInfo()
                {
                    FullName = resumeDto.PersonalInfo.FullName,
                    Address = resumeDto.PersonalInfo.Address,
                    Email = resumeDto.PersonalInfo.Email,
                    PhoneNumber = resumeDto.PersonalInfo.PhoneNumber
                };
                personalInfo.ResumeId = resume.Id;
                _dbContext.PersonalInfos.Add(personalInfo);
            }

            // Create and associate Educations
            if (resumeDto.Educations != null)
            {
                foreach (var educationDto in resumeDto.Educations)
                {
                    //var education = _mapper.Map<Education>(educationDto);
                    var education = new Education()
                    {
                        InstituteName = educationDto.InstituteName,
                        Degree = educationDto.Degree,
                        FieldOfStudy = educationDto.FieldOfStudy
                    };

                    education.ResumeId = resume.Id;
                    _dbContext.Education.Add(education);
                }
            }

            // Create and associate WorkExperiences
            if (resumeDto.WorkExperiences != null)
            {
                foreach (var experienceDto in resumeDto.WorkExperiences)
                {
                    //var workExperience = _mapper.Map<WorkExperience>(experienceDto);
                    var workExperience = new WorkExperience()
                    {
                        Position = experienceDto.Position,
                        CompanyName = experienceDto.CompanyName,
                        Description = experienceDto.Description
                    };
                    workExperience.ResumeId = resume.Id;
                    _dbContext.WorkExperiences.Add(workExperience);
                }
            }

            // Create and associate Languages
            if (resumeDto.Languages != null)
            {
                foreach (var languageDto in resumeDto.Languages)
                {
                    //var language = _mapper.Map<Languages>(languageDto);
                    var language = new Languages()
                    {
                        LanguageName = languageDto.LanguageName,
                        ProficiencyLevel = languageDto.ProficiencyLevel
                    };

                    _dbContext.Languages.Add(language);
                    await _dbContext.SaveChangesAsync();

                    foreach (var item in _dbContext.Languages)
                    {
                        if (item.LanguageName == languageDto.LanguageName && item.ProficiencyLevel == languageDto.ProficiencyLevel)
                        {
                            language.Id = item.Id;
                            break;
                        }
                    }


                    var resumeLanguage = new ResumeLanguages
                    {
                        ResumeId = resume.Id,
                        LanguageId = language.Id
                    };

                    //var resumeLanguage = _mapper.Map<ResumeLanguages>(languageDto);
                    //resumeLanguage.ResumeId = resume.Id;

                    _dbContext.ResumeLanguages.Add(resumeLanguage);
                }
            }

            // Create and associate Skills
            if (resumeDto.Skills != null)
            {
                foreach (var skillDto in resumeDto.Skills)
                {
                    //var skill = _mapper.Map<Skills>(skillDto);
                    var skill = new Skills()
                    {
                        SkillName = skillDto.SkillName
                    };


                    _dbContext.Skills.Add(skill);
                    await _dbContext.SaveChangesAsync();

                    foreach (var item in _dbContext.Skills)
                    {
                        if(item.SkillName == skillDto.SkillName)
                        {
                            skill.Id = item.Id;
                            break;
                        }
                    }

                    var resumeSkill = new ResumeSkills
                    {
                        ResumeId = resume.Id,
                        SkillId = skill.Id
                    };

                    //var resumeSkill = _mapper.Map<ResumeSkills>(skillDto);
                    //resumeSkill.ResumeId = resume.Id;

                    _dbContext.ResumeSkills.Add(resumeSkill);
                }
            }

            // Create and associate Locations
            if (resumeDto.Locations != null)
            {
                foreach (var locationDto in resumeDto.Locations)
                {
                    //var location = _mapper.Map<Locations>(locationDto);
                    var location = new Locations()
                    {
                        City = locationDto.City,
                        State = locationDto.State,
                        Country = locationDto.Country
                    };

                    _dbContext.Locations.Add(location);
                    await _dbContext.SaveChangesAsync();

                    foreach (var item in _dbContext.Locations)
                    {
                        if(item.City == locationDto.City && item.State == locationDto.State && item.Country == locationDto.Country)
                        {
                            location.Id = item.Id;
                        }
                    }

                    var resumeLocation = new ResumeLocations
                    {
                        ResumeId = resume.Id,
                        LocationId = location.Id
                    }; 

                    //var resumeLocation = _mapper.Map<ResumeLocations>(locationDto);
                    //resumeLocation.ResumeId = resume.Id;

                    _dbContext.ResumeLocations.Add(resumeLocation);
                }
            }

            // Create and associate Certificates
            if (resumeDto.Certificates != null)
            {
                foreach (var certificateDto in resumeDto.Certificates)
                {
                    //var certificate = _mapper.Map<Certificates>(certificateDto);
                    var certificate = new Certificates()
                    {
                        CertificateName = certificateDto.CertificateName,
                        IssueDate = certificateDto.IssueDate,
                        IssuingOrganization = certificateDto.IssuingOrganization
                    };

                    certificate.ResumeId = resume.Id;
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
