using Microsoft.EntityFrameworkCore;
using Online_CV_Builder.Data.Entities;

namespace Online_CV_Builder.Data
{
	public class DataSeeder
	{
		private readonly ResumeBuilderContext _dbContext;
		public DataSeeder(ResumeBuilderContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void Seed()
		{
			if (!_dbContext.Resumes.Any())
			{
				var resumes = new List<Resumes>
				{
					new Resumes {UserId = 1, Title = "John's Resume", CreationDate = DateTime.Now, LastModifiedDate = DateTime.Now },
					new Resumes {UserId = 2, Title = "Jane's Resume", CreationDate = DateTime.Now, LastModifiedDate = DateTime.Now }
				};
				_dbContext.Resumes.AddRange(resumes);
				_dbContext.SaveChanges();
			}
			if (!_dbContext.PersonalInfos.Any())
			{
				var personalInfos = new List<PersonalInfo>
				{
					new PersonalInfo {ResumeId = _dbContext.Resumes.FirstOrDefault(r => r.UserId == 1).Id, FullName = "John Doe", PhoneNumber = "123456789", Email = "john.doe@example.com" },
						new PersonalInfo { ResumeId = _dbContext.Resumes.FirstOrDefault(r => r.UserId == 2).Id, FullName = "Jane Smith", PhoneNumber = "987654321", Email = "jane.smith@example.com" }
				};
				_dbContext.PersonalInfos.AddRange(personalInfos);
				_dbContext.SaveChanges();
			}			

			if (!_dbContext.WorkExperiences.Any())
			{
				var workExperiences = new List<WorkExperience>
				{
					new WorkExperience { ResumeId = _dbContext.Resumes.FirstOrDefault(r => r.UserId == 1).Id, CompanyName = "ABC Corp", Position = "Software Developer", StartDate = new DateTime(2019, 6, 1), EndDate = new DateTime(2021, 12, 31), Description = "Developed web applications using ASP.NET Core" },
						new WorkExperience { ResumeId = _dbContext.Resumes.FirstOrDefault(r => r.UserId == 2).Id, CompanyName = "XYZ Inc", Position = "Project Manager", StartDate = new DateTime(2020, 6, 1), EndDate = new DateTime(2022, 12, 31), Description = "Managed software development projects" }
				};
				_dbContext.WorkExperiences.AddRange(workExperiences);
				_dbContext.SaveChanges();

				var languages = new List<Languages>
				{
					new Languages { LanguageName = "English", ProficiencyLevel = "Fluent" },
						new Languages { LanguageName = "French", ProficiencyLevel = "Intermediate" }
				};
				_dbContext.Languages.AddRange(languages);
				_dbContext.SaveChanges();
			}

			if (!_dbContext.Skills.Any())
			{
				var skills = new List<Skills>
				{
					new Skills { SkillName = "C#" },
						new Skills { SkillName = "JavaScript" },
						new Skills { SkillName = "Project Management" }
				};
				_dbContext.Skills.AddRange(skills);
				_dbContext.SaveChanges();

				var locations = new List<Locations>
				{
					new Locations { City = "New York", State = "NY", Country = "USA" },
						new Locations {City = "Paris", State = "", Country = "France" }
				};
				_dbContext.Locations.AddRange(locations);
				_dbContext.SaveChanges();
			}

			if (!_dbContext.ResumeSkills.Any())
			{
				var resumeSkills = new List<ResumeSkills>
				{
					new ResumeSkills { ResumeId = _dbContext.Resumes.FirstOrDefault(r => r.UserId == 1).Id, SkillId = _dbContext.Skills.FirstOrDefault(s => s.SkillName == "C#").Id },
						new ResumeSkills { ResumeId = _dbContext.Resumes.FirstOrDefault(r => r.UserId == 2).Id, SkillId = _dbContext.Skills.FirstOrDefault(s => s.SkillName == "JavaScript").Id },
						new ResumeSkills { ResumeId = _dbContext.Resumes.FirstOrDefault(r => r.UserId == 2).Id, SkillId = _dbContext.Skills.FirstOrDefault(s => s.SkillName == "Project Management").Id }
				};
				_dbContext.ResumeSkills.AddRange(resumeSkills);
				_dbContext.SaveChanges();
			}

			if (!_dbContext.ResumeLanguages.Any())
			{
				var resumeLanguages = new List<ResumeLanguages>
				{
					new ResumeLanguages { ResumeId = _dbContext.Resumes.FirstOrDefault(r => r.UserId == 1).Id, LanguageId = _dbContext.Languages.FirstOrDefault(l => l.LanguageName == "English").Id },
						new ResumeLanguages { ResumeId = _dbContext.Resumes.FirstOrDefault(r => r.UserId == 2).Id, LanguageId = _dbContext.Languages.FirstOrDefault(l => l.LanguageName == "English").Id },
						new ResumeLanguages { ResumeId = _dbContext.Resumes.FirstOrDefault(r => r.UserId == 2).Id, LanguageId = _dbContext.Languages.FirstOrDefault(l => l.LanguageName == "French").Id }
				};
				_dbContext.ResumeLanguages.AddRange(resumeLanguages);
				_dbContext.SaveChanges();
			}

			if (!_dbContext.ResumeLocations.Any())
			{
				var resumeLocations = new List<ResumeLocations>
				{
					new ResumeLocations { ResumeId = _dbContext.Resumes.FirstOrDefault(r => r.UserId == 1).Id, LocationId = _dbContext.Locations.FirstOrDefault(l => l.City == "New York").Id },
						new ResumeLocations { ResumeId = _dbContext.Resumes.FirstOrDefault(r => r.UserId == 2).Id, LocationId = _dbContext.Locations.FirstOrDefault(l => l.City == "Paris").Id }
				};
				_dbContext.ResumeLocations.AddRange(resumeLocations);
				_dbContext.SaveChanges();
			}

			if (!_dbContext.Education.Any())
			{
				var educations = new List<Education>
				{
					 new Education {ResumeId = _dbContext.Resumes.FirstOrDefault(r => r.UserId == 1).Id, InstituteName = "University of ABC", Degree = "Bachelor's Degree", FieldOfStudy = "Computer Science", StartDate = new DateTime(2015, 9, 1), EndDate = new DateTime(2019, 5, 31) },
						new Education { ResumeId = _dbContext.Resumes.FirstOrDefault(r => r.UserId == 2).Id, InstituteName = "University of XYZ", Degree = "Master's Degree", FieldOfStudy = "Business Administration", StartDate = new DateTime(2018, 9, 1), EndDate = new DateTime(2020, 5, 31) }
				};
				_dbContext.Education.AddRange(educations);
				_dbContext.SaveChanges();
			}

			if (!_dbContext.Certificates.Any())
			{
				var certificates = new List<Certificates>
				{
					new Certificates {ResumeId = _dbContext.Resumes.FirstOrDefault(r => r.UserId == 1).Id, CertificateName = "Microsoft Certified Professional", IssuingOrganization = "Microsoft", IssueDate = new DateTime(2020, 1, 15) },
						new Certificates { ResumeId = _dbContext.Resumes.FirstOrDefault(r => r.UserId == 2).Id, CertificateName = "Project Management Professional (PMP)", IssuingOrganization = "PMI", IssueDate = new DateTime(2021, 2, 20) }
				};
				_dbContext.Certificates.AddRange(certificates);
				_dbContext.SaveChanges();
			}

			if (!_dbContext.Templates.Any())
			{
				var templates = new List<Templates>
				{
					new Templates
					{
						TemplateName = "Template1",
						TemplateFilePath = @"C:/Online CV Builder/Online CV Builder/Online CV Builder Web Api/Templates/Template1/Template1.cshtml"
					},
					new Templates
					{
						TemplateName = "Template2",
						TemplateFilePath = @"C:/Online CV Builder/Online CV Builder/Online CV Builder Web Api/Templates/Template2/Template2.cshtml"
					},
					new Templates
					{
						TemplateName = "Template3",
						TemplateFilePath = @"C:/Online CV Builder/Online CV Builder/Online CV Builder Web Api/Templates/Template3/Template3.cshtml"
					}

				};
				_dbContext.Templates.AddRange(templates);
				_dbContext.SaveChanges();
			}

			if (!_dbContext.ResumeTemplates.Any())
			{
				var resumeTemplates = new List<ResumeTemplates>
				{
					 new ResumeTemplates { ResumeId = _dbContext.Resumes.FirstOrDefault(r => r.UserId == 1).Id, TemplateId = _dbContext.Templates.FirstOrDefault(t => t.TemplateName == "Template1").Id },
						new ResumeTemplates { ResumeId = _dbContext.Resumes.FirstOrDefault(r => r.UserId == 2).Id, TemplateId = _dbContext.Templates.FirstOrDefault(t => t.TemplateName == "Template2").Id }
				};
				_dbContext.ResumeTemplates.AddRange(resumeTemplates);
				_dbContext.SaveChanges();
			}
			
		}
	}
}
