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
            // handling validations
            var validationContext = new ValidationContext(resumeDto, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(resumeDto, validationContext, validationResults, validateAllProperties: true);

            // map the values of resumeDto to Resumes
            var resume = _mapper.Map<Resumes>(resumeDto);
            resume.CreationDate = DateTime.Now;
            resume.LastModifiedDate = DateTime.Now;

            _dbContext.Resumes.Add(resume);
            await _dbContext.SaveChangesAsync();

            // Create and associate PersonalInfo
            if (resumeDto.PersonalInfo != null)
            {
                // map the values of personalInfoDto to personalInfo
                var personalInfo = _mapper.Map<PersonalInfo>(resumeDto.PersonalInfo);
                personalInfo.ResumeId = resume.Id;
                _dbContext.PersonalInfos.Add(personalInfo);
            }

            // Create and associate Educations
            if (resumeDto.Educations != null)
            {
                // handling multiple education input
                foreach (var educationDto in resumeDto.Educations)
                {
                    // map the values of educationDto to education
                    var education = _mapper.Map<Education>(educationDto);
                    education.ResumeId = resume.Id;
                    _dbContext.Education.Add(education);
                }
            }

            // Create and associate WorkExperiences
            if (resumeDto.WorkExperiences != null)
            {
                // handling multiple experience input
                foreach (var experienceDto in resumeDto.WorkExperiences)
                {
                    // map the values of experienceDto to workExperience
                    var workExperience = _mapper.Map<WorkExperience>(experienceDto);
                    workExperience.ResumeId = resume.Id;
                    _dbContext.WorkExperiences.Add(workExperience);
                }
            }

            // Create and associate Languages
            if (resumeDto.Languages != null)
            {
                // handling multiple language input
                foreach (var languageDto in resumeDto.Languages)
                {
                    // map the values of languageDto to Languages
                    var language = _mapper.Map<Languages>(languageDto);

                    // checking if language already exists
                    bool languageExist = false;
                    foreach (var item in _dbContext.Languages)
                    {
                        if (item.LanguageName == languageDto.LanguageName && item.ProficiencyLevel == languageDto.ProficiencyLevel)
                        {
                            languageExist = true;  
                            break;
                        }
                    }

                    // adding new language
                    if(!languageExist)
                    {
                        _dbContext.Languages.Add(language);
                        await _dbContext.SaveChangesAsync();
                    }
                    
                    // assigning the ID to language to be able to record it in the connection table
                    foreach (var item in _dbContext.Languages)
                    {
                        if (item.LanguageName == languageDto.LanguageName && item.ProficiencyLevel == languageDto.ProficiencyLevel)
                        {
                            language.Id = item.Id;
                            break;
                        }
                    }

                    // assigning the IDs in the connection table
                    var resumeLanguage = new ResumeLanguages
                    {
                        ResumeId = resume.Id,
                        LanguageId = language.Id
                    };

                    _dbContext.ResumeLanguages.Add(resumeLanguage);
                }
            }

            // Create and associate Skills
            if (resumeDto.Skills != null)
            {
                // handling multiple skill input
                foreach (var skillDto in resumeDto.Skills)
                {
                    // map the values of skillDto to Skills
                    var skill = _mapper.Map<Skills>(skillDto);
                    
                    // check if skill exist in the db
                    bool skillExist = false;
                    foreach (var item in _dbContext.Skills)
                    {
                        if (item.SkillName == skillDto.SkillName)
                        {
                            skillExist = true;
                            break;
                        }
                    }

                    // if the skill doesn't exist we add it to the db
                    if (!skillExist)
                    {
                        _dbContext.Skills.Add(skill);
                        await _dbContext.SaveChangesAsync();
                    }

                    // assigning ID to skill to be able to set it in the connected table
                    foreach (var item in _dbContext.Skills)
                    {
                        if(item.SkillName == skillDto.SkillName)
                        {
                            skill.Id = item.Id;
                            break;
                        }
                    }

                    // assigning the IDs in the connected table
                    var resumeSkill = new ResumeSkills
                    {
                        ResumeId = resume.Id,
                        SkillId = skill.Id
                    };

                    _dbContext.ResumeSkills.Add(resumeSkill);
                }
            }

            // Create and associate Locations
            if (resumeDto.Locations != null)
            {
                // handling multiple location input
                foreach (var locationDto in resumeDto.Locations)
                {
                    // map the values of locationDto to Locations
                    var location = _mapper.Map<Locations>(locationDto);

                    // check if location exist in the db
                    bool locationExist = false;
                    foreach (var item in _dbContext.Locations)
                    {
                        if (item.City == locationDto.City && item.State == locationDto.State && item.Country == locationDto.Country)
                        {
                            locationExist = true;
                            break;
                        }
                    }

                    // if the location doesn't exist we add it to the db
                    if (!locationExist)
                    {
                        _dbContext.Locations.Add(location);
                        await _dbContext.SaveChangesAsync();
                    }

                    // assigning ID to location to be able to set it in the connected table
                    foreach (var item in _dbContext.Locations)
                    {
                        if(item.City == locationDto.City && item.State == locationDto.State && item.Country == locationDto.Country)
                        {
                            location.Id = item.Id;
                        }
                    }

                    // assigning the IDs in the connected table
                    var resumeLocation = new ResumeLocations
                    {
                        ResumeId = resume.Id,
                        LocationId = location.Id
                    }; 

                    _dbContext.ResumeLocations.Add(resumeLocation);
                }
            }

            // Create and associate Certificates
            if (resumeDto.Certificates != null)
            {
                // handling multiple certificate input
                foreach (var certificateDto in resumeDto.Certificates)
                {
                    // map the values of certificateDto to Certificates
                    var certificate = _mapper.Map<Certificates>(certificateDto);
                    certificate.ResumeId = resume.Id;
                    _dbContext.Certificates.Add(certificate);
                }
            }

            // Save changes after adding entities
            await _dbContext.SaveChangesAsync();

            return resume;
        }

        public async Task<ResumeDTO> GetResumeAsync(int resumeId)
        {
            // Retrieve the resume from the database based on the resumeId
            var resumeEntity = await _dbContext.Resumes.FindAsync(resumeId);
            var resumeDto = new ResumeDTO()
            {
                //UserId = (int)resumeEntity.UserId,
                CreationDate = (DateTime)resumeEntity.CreationDate,
                LastModifiedDate = (DateTime)resumeEntity.LastModifiedDate
            };

            if (resumeEntity == null)
            {
                return null; // Resume not found
            }

            // Retrieve the related entities using separate queries
            var personalInfo = await _dbContext.PersonalInfos.FirstOrDefaultAsync(pi => pi.ResumeId == resumeId);
            var educations = await _dbContext.Education.Where(e => e.ResumeId == resumeId).ToListAsync();
            var workExperiences = await _dbContext.WorkExperiences.Where(we => we.ResumeId == resumeId).ToListAsync();
            var languages = await _dbContext.ResumeLanguages
                .Include(rl => rl.Language)
                .Where(rl => rl.ResumeId == resumeId)
                .Select(rl => rl.Language)
                .ToListAsync();
            var skills = await _dbContext.ResumeSkills
                .Include(rs => rs.Skill)
                .Where(rs => rs.ResumeId == resumeId)
                .Select(rs => rs.Skill)
                .ToListAsync();
            var locations = await _dbContext.ResumeLocations
                .Include(rl => rl.Location)
                .Where(rl => rl.ResumeId == resumeId)
                .Select(rl => rl.Location)
                .ToListAsync();
            var certificates = await _dbContext.Certificates.Where(c => c.ResumeId == resumeId).ToListAsync();

            PersonalInfoDTO personalInfoDto = new PersonalInfoDTO();
            personalInfoDto = _mapper.Map<PersonalInfoDTO>(personalInfo);

            List<EducationDTO> educationDTOs = new List<EducationDTO>();
            educationDTOs = _mapper.Map<List<EducationDTO>>(educations);

            List<WorkExperienceDTO> workExperienceDTOs = new List<WorkExperienceDTO>();
            workExperienceDTOs = _mapper.Map<List<WorkExperienceDTO>>(workExperiences);

            List<LanguageDTO> languageDTOs = new List<LanguageDTO>();
            languageDTOs = _mapper.Map<List<LanguageDTO>>(languages);

            List<SkillDTO> skillDTOs = new List<SkillDTO>();
            skillDTOs = _mapper.Map<List<SkillDTO>>(skills);

            List<LocationDTO> locationDTOs = new List<LocationDTO>();
            locationDTOs = _mapper.Map<List<LocationDTO>>(locations);

            List<CertificateDTO> certificateDTOs = new List<CertificateDTO>();
            certificateDTOs = _mapper.Map<List<CertificateDTO>>(certificates);

            // Assign the retrieved related entities to the resume object
            resumeDto.PersonalInfo = personalInfoDto;
            resumeDto.Educations = educationDTOs;
            resumeDto.WorkExperiences = workExperienceDTOs;
            resumeDto.Languages = languageDTOs;
            resumeDto.Skills = skillDTOs;
            resumeDto.Locations = locationDTOs;
            resumeDto.Certificates = certificateDTOs;

            return resumeDto;
        }

        public async Task<bool> DeleteResumeAsync(int resumeId)
        {
            var resume = await _dbContext.Resumes.FindAsync(resumeId);
            if (resume == null)
                return false;

            // Remove related entities
            _dbContext.PersonalInfos.RemoveRange(_dbContext.PersonalInfos.Where(pi => pi.ResumeId == resumeId));
            _dbContext.Education.RemoveRange(_dbContext.Education.Where(ed => ed.ResumeId == resumeId));
            _dbContext.WorkExperiences.RemoveRange(_dbContext.WorkExperiences.Where(we => we.ResumeId == resumeId));
            _dbContext.Certificates.RemoveRange(_dbContext.Certificates.Where(c => c.ResumeId == resumeId));

            // Remove connected entities
            _dbContext.ResumeLanguages.RemoveRange(_dbContext.ResumeLanguages.Where(rl => rl.ResumeId == resumeId));
            _dbContext.ResumeSkills.RemoveRange(_dbContext.ResumeSkills.Where(rs => rs.ResumeId == resumeId));
            _dbContext.ResumeLocations.RemoveRange(_dbContext.ResumeLocations.Where(rl => rl.ResumeId == resumeId));

            _dbContext.Resumes.Remove(resume);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        
    }
}
