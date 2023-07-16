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

        public async Task<Resumes> UpdateResumeAsync(int resumeId, ResumeDTO resumeDto)
        {
            var resume = await _dbContext.Resumes.FindAsync(resumeId);
            _dbContext.Resumes.Attach(resume);
            if (resume == null)
            {
                return null;
            }

            // Update the properties of the resume entity
            resume.Title = resumeDto.Title;
            resume.LastModifiedDate = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            // Update the related entities
            if (resumeDto.PersonalInfo != null)
            {
                var personalInfo = await _dbContext.PersonalInfos.FirstOrDefaultAsync(p => p.ResumeId == resumeId);
                _dbContext.PersonalInfos.Attach(personalInfo);
                if (personalInfo != null)
                {
                    // map the values of personalInfoDto to personalInfo

                    //personalInfo = _mapper.Map<PersonalInfo>(resumeDto.PersonalInfo);
                    var oldPersonalInfo = resumeDto.PersonalInfo;
                    personalInfo.FullName = oldPersonalInfo.FullName;
                    personalInfo.Address = oldPersonalInfo.Address;
                    personalInfo.PhoneNumber = oldPersonalInfo.PhoneNumber;
                    personalInfo.Email = oldPersonalInfo.Email;
                    await _dbContext.SaveChangesAsync();
                }
            }

            if(resumeDto.WorkExperiences != null)
            {
                var workExperiences = await _dbContext.WorkExperiences
                        .Where(e => e.ResumeId == resumeId)
                        .ToListAsync();
                _dbContext.WorkExperiences.AttachRange(workExperiences);
                var newWorkExperiences = resumeDto.WorkExperiences.ToList();
                int numberOfOldWorkExperiences = workExperiences.Count;
                int numberOfNewWorkExperiences = newWorkExperiences.Count;

                
                for (int i = 0; i < numberOfOldWorkExperiences; i++)
                {
                    //old records are more than the new records
                    if(i > numberOfNewWorkExperiences - 1)
                    {
                        _dbContext.WorkExperiences.Remove(workExperiences[i]);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        
                        workExperiences[i].Description = newWorkExperiences[i].Description;
                        workExperiences[i].Position = newWorkExperiences[i].Position;
                        workExperiences[i].CompanyName = newWorkExperiences[i].CompanyName;
                        workExperiences[i].StartDate = newWorkExperiences[i].StartDate;
                        workExperiences[i].EndDate = newWorkExperiences[i].EndDate;

                        
                        await _dbContext.SaveChangesAsync();
                    }
                }

                // new records are more than the old records
                if(numberOfOldWorkExperiences < numberOfNewWorkExperiences)
                {
                    for (int i = numberOfOldWorkExperiences; i < numberOfNewWorkExperiences; i++)
                    {
                        var newWorkExperienceEntity = _mapper.Map<WorkExperience>(newWorkExperiences[i]);
                        _dbContext.WorkExperiences.Add(newWorkExperienceEntity);
                    }
                    await _dbContext.SaveChangesAsync();
                }
                
            }

            if(resumeDto.Educations != null)
            {
                var educations = await _dbContext.Education
                        .Where(e => e.ResumeId == resumeId)
                        .ToListAsync();
                _dbContext.Education.AttachRange(educations);
                var newEducations = resumeDto.Educations.ToList();
                int numberOfOldEducations = educations.Count;
                int numberOfNewEducations = newEducations.Count;


                for (int i = 0; i < numberOfOldEducations; i++)
                {
                    //old records are more than the new records
                    if (i > numberOfNewEducations - 1)
                    {
                        _dbContext.Education.Remove(educations[i]);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        educations[i].InstituteName = newEducations[i].FieldOfStudy;
                        educations[i].Degree = newEducations[i].Degree;
                        educations[i].FieldOfStudy = newEducations[i].FieldOfStudy;
                        educations[i].StartDate = newEducations[i].StartDate;
                        educations[i].EndDate = newEducations[i].EndDate;
                        await _dbContext.SaveChangesAsync();
                    }
                }

                // new records are more than the old records
                if (numberOfOldEducations < numberOfNewEducations)
                {
                    for (int i = numberOfOldEducations; i < numberOfNewEducations; i++)
                    {
                        var newEducationEntity = _mapper.Map<Education>(newEducations[i]);
                        _dbContext.Education.Add(newEducationEntity);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }

            if(resumeDto.Certificates != null)
            {
                var certificates = await _dbContext.Certificates
                        .Where(c => c.ResumeId == resumeId)
                        .ToListAsync();
                _dbContext.Certificates.AttachRange(certificates);
                var newCertificates = resumeDto.Certificates.ToList();
                int numberOfOldCertificates = certificates.Count;
                int numberOfNewCertificates = certificates.Count;


                for (int i = 0; i < numberOfOldCertificates; i++)
                {
                    //old records are more than the new records
                    if (i > numberOfNewCertificates - 1)
                    {
                        _dbContext.Certificates.Remove(certificates[i]);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        certificates[i].CertificateName = newCertificates[i].CertificateName;
                        certificates[i].IssuingOrganization = newCertificates[i].IssuingOrganization;
                        certificates[i].IssueDate = newCertificates[i].IssueDate;
                        
                        await _dbContext.SaveChangesAsync();
                    }
                }

                // new records are more than the old records
                if (numberOfOldCertificates < numberOfNewCertificates)
                {
                    for (int i = numberOfOldCertificates; i < numberOfNewCertificates; i++)
                    {
                        var newCertificateEntity = _mapper.Map<Certificates>(certificates[i]);
                        _dbContext.Certificates.Add(newCertificateEntity);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }

            if(resumeDto.Languages != null)
            {
                var updatedLanguages = resumeDto.Languages.ToList();
               
                var resumeLanguages = await _dbContext.ResumeLanguages.Where(rl => rl.ResumeId == resumeId).ToListAsync();
                _dbContext.ResumeLanguages.AttachRange(resumeLanguages);
                int numberOfUpdatedLanguages = updatedLanguages.Count;
                int numberOfOldLanguages = resumeLanguages.Count;
               
                int numberOfLanguage = 0;
                foreach(var newLanguage in updatedLanguages)
                {
                    int? oldLanguageId = null;
                    var oldLanguages = _dbContext.Languages;

                    // checking if there is currently such a language in the db
                    foreach (var oldLanguage in oldLanguages)
                    {
                        if(oldLanguage.LanguageName == newLanguage.LanguageName &&  oldLanguage.ProficiencyLevel == newLanguage.ProficiencyLevel)
                        {
                            oldLanguageId = oldLanguage.Id;
                            //resumeLanguages[numberOfLanguage].LanguageId = oldLanguageId;
                            _dbContext.ResumeLanguages.Remove(resumeLanguages[numberOfLanguage]);
                            var newResumeLanguage = new ResumeLanguages()
                            {
                                LanguageId = oldLanguageId,
                                ResumeId = resumeId
                            };
                            _dbContext.ResumeLanguages.Add(newResumeLanguage);
                            
                            break;
                        }
                    }
                    await _dbContext.SaveChangesAsync();

                    // if there isn't such a language in the db
                    if(oldLanguageId == null)
                    {
                            var newLanguageEntity = _mapper.Map<Languages>(newLanguage);
                            _dbContext.Languages.Add(newLanguageEntity);
                            await _dbContext.SaveChangesAsync();
                            int? newLanguageId = null;
                            foreach (var item in _dbContext.Languages)
                            {
                                if (item.LanguageName == newLanguage.LanguageName && item.ProficiencyLevel == newLanguage.ProficiencyLevel)
                                {
                                    newLanguageId = item.Id;
                                    break;
                                }
                            }

                            // if we have more languages than the old ones
                            if(numberOfLanguage > numberOfOldLanguages - 1)
                            {
                                var resumeLanguage = new ResumeLanguages()
                                {
                                    ResumeId = resumeId,
                                    LanguageId = newLanguageId
                                };
                                _dbContext.ResumeLanguages.Add(resumeLanguage);
                                await _dbContext.SaveChangesAsync();
                            }
                            else
                            {
                                // we update the value of the oldLanguageId
                                _dbContext.ResumeLanguages.Remove(resumeLanguages[numberOfLanguage]);
                                var newResumeLanguage = new ResumeLanguages()
                                {
                                    LanguageId = newLanguageId,
                                    ResumeId = resumeId
                                };
                                _dbContext.ResumeLanguages.Add(newResumeLanguage);
                                //resumeLanguages[numberOfLanguage].LanguageId = newLanguageId;
                                await _dbContext.SaveChangesAsync();
                            }

                    }
                    numberOfLanguage++;
       
                }

                //if we remove several languages in the process of updating
                if(numberOfUpdatedLanguages < numberOfOldLanguages)
                {
                    for(int i = numberOfUpdatedLanguages; i < numberOfOldLanguages; i++)
                    {
                        var removedLanguage = _dbContext.ResumeLanguages.Where(rl => rl.ResumeId == resumeId && rl.LanguageId == resumeLanguages[i].LanguageId).FirstOrDefault();
                        _dbContext.ResumeLanguages.Remove(removedLanguage);
                        await _dbContext.SaveChangesAsync();
                    }
                    
                }
                
            }

            if(resumeDto.Skills != null)
            {
                var updatedSkills = resumeDto.Skills.ToList();
                var resumeSkills = await _dbContext.ResumeSkills.Where(rs => rs.ResumeId == resumeId).ToListAsync();
                _dbContext.ResumeSkills.AttachRange(resumeSkills);
                int numberOfUpdatedSkills = updatedSkills.Count;
                int numberOfOldSkills = resumeSkills.Count;

                int numberOfSkill = 0;
                foreach (var newSkill in updatedSkills)
                {
                    int? oldSkillId = null;
                    var oldSkills = _dbContext.Skills;

                    // checking if there is currently such a skill in the db
                    foreach (var oldSkill in oldSkills)
                    {
                        if (oldSkill.SkillName == newSkill.SkillName)
                        {
                            oldSkillId = oldSkill.Id;
                            _dbContext.ResumeSkills.Remove(resumeSkills[numberOfSkill]);
                            var newResumeSkill = new ResumeSkills()
                            {
                                SkillId = oldSkillId,
                                ResumeId = resumeId
                            };
                            _dbContext.ResumeSkills.Add(newResumeSkill);
                            //resumeSkills[numberOfSkill].SkillId = oldSkillId;
                            
                            break;
                        }
                    }
                    await _dbContext.SaveChangesAsync();

                    // if there isn't such a skill in the db
                    if (oldSkillId == null)
                    {
                        var newSkillEntity = _mapper.Map<Skills>(newSkill);
                        _dbContext.Skills.Add(newSkillEntity);
                        await _dbContext.SaveChangesAsync();
                        int? newSkillId = null;
                        foreach (var item in _dbContext.Skills)
                        {
                            if (item.SkillName == newSkill.SkillName)
                            {
                                newSkillId = item.Id;
                                break;
                            }
                        }

                        // if we have more skills than the old ones
                        if (numberOfSkill > numberOfOldSkills - 1)
                        {
                            var resumeSkill = new ResumeSkills()
                            {
                                ResumeId = resumeId,
                                SkillId = newSkillId
                            };
                            _dbContext.ResumeSkills.Add(resumeSkill);
                            await _dbContext.SaveChangesAsync();
                        }
                        else
                        {
                            // we update the value of the oldSkillId
                            _dbContext.ResumeSkills.Remove(resumeSkills[numberOfSkill]);
                            var newResumeSkill = new ResumeSkills()
                            {
                                SkillId = newSkillId,
                                ResumeId = resumeId
                            };
                            _dbContext.ResumeSkills.Add(newResumeSkill);
                            //resumeSkills[numberOfSkill].SkillId = newSkillId;
                            await _dbContext.SaveChangesAsync();
                        }

                    }
                    numberOfSkill++;

                }

                //if we remove several skills in the process of updating
                if (numberOfUpdatedSkills < numberOfOldSkills)
                {
                    for (int i = numberOfUpdatedSkills; i < numberOfOldSkills; i++)
                    {
                        var removedSkill = _dbContext.ResumeSkills.Where(rl => rl.ResumeId == resumeId && rl.SkillId == resumeSkills[i].SkillId).FirstOrDefault();
                        _dbContext.ResumeSkills.Remove(removedSkill);
                        await _dbContext.SaveChangesAsync();
                    }
                    
                }
            }

            if (resumeDto.Locations != null)
            {
                var updatedLocations = resumeDto.Locations.ToList();
                var resumeLocations = await _dbContext.ResumeLocations.Where(rl => rl.ResumeId == resumeId).ToListAsync();
                _dbContext.ResumeLocations.AttachRange(resumeLocations);
                int numberOfUpdatedLocations = updatedLocations.Count;
                int numberOfOldLocations = resumeLocations.Count;

                int numberOfLocation = 0;
                foreach (var newLocation in updatedLocations)
                {
                    int? oldLocationId = null;
                    var oldLocations = _dbContext.Locations;

                    // checking if there is currently such a location in the db
                    foreach (var oldLocation in oldLocations)
                    {
                        if (oldLocation.City == newLocation.City && oldLocation.State == newLocation.State && oldLocation.Country == newLocation.Country)
                        {
                            oldLocationId = oldLocation.Id;
                            _dbContext.ResumeLocations.Remove(resumeLocations[numberOfLocation]);
                            var newResumeLocation = new ResumeLocations()
                            {
                                LocationId = oldLocationId,
                                ResumeId = resumeId
                            };
                            _dbContext.ResumeLocations.Add(newResumeLocation);
                            //resumeLocations[numberOfLocation].LocationId = oldLocationId;
                            
                            break;
                        }
                    }
                    await _dbContext.SaveChangesAsync();

                    // if there isn't such a location in the db
                    if (oldLocationId == null)
                    {
                        var newLocationEntity = _mapper.Map<Locations>(newLocation);
                        _dbContext.Locations.Add(newLocationEntity);
                        await _dbContext.SaveChangesAsync();
                        int? newLocationId = null;
                        foreach (var item in _dbContext.Locations)
                        {
                            if (item.City == newLocation.City && item.State == newLocation.State && item.Country == newLocation.Country)
                            {
                                newLocationId = item.Id;
                                break;
                            }
                        }

                        // if we have more skills than the old ones
                        if (numberOfLocation > numberOfOldLocations - 1)
                        {
                            var resumeLocation = new ResumeLocations()
                            {
                                ResumeId = resumeId,
                                LocationId = newLocationId
                            };
                            _dbContext.ResumeLocations.Add(resumeLocation);
                            await _dbContext.SaveChangesAsync();
                        }
                        else
                        {
                            // we update the value of the oldSkillId
                            _dbContext.ResumeLocations.Remove(resumeLocations[numberOfLocation]);
                            var newResumeLocation = new ResumeLocations()
                            {
                                LocationId = newLocationId,
                                ResumeId = resumeId
                            };
                            _dbContext.ResumeLocations.Add(newResumeLocation);
                            //resumeLocations[numberOfLocation].LocationId = newLocationId;
                            await _dbContext.SaveChangesAsync();
                        }

                    }
                    numberOfLocation++;

                }

                //if we remove several skills in the process of updating
                if (numberOfUpdatedLocations < numberOfOldLocations)
                {
                    for (int i = numberOfUpdatedLocations; i < numberOfOldLocations; i++)
                    {
                        var removedLocation = _dbContext.ResumeLocations.Where(rl => rl.ResumeId == resumeId && rl.LocationId == resumeLocations[i].LocationId).FirstOrDefault();
                        _dbContext.ResumeLocations.Remove(removedLocation);
                        await _dbContext.SaveChangesAsync();
                    }
       
                }
            }
            
            await _dbContext.SaveChangesAsync();

            return resume;
        }
    }
}
