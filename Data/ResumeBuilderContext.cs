using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Online_CV_Builder.Data.Entities;
using Online_CV_Builder.Models;

namespace Online_CV_Builder.Data
{
    public class ResumeBuilderContext : DbContext
    {
        public ResumeBuilderContext(DbContextOptions<ResumeBuilderContext> options) : base(options)
        {
            
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Resumes> Resumes { get; set; }
        public DbSet<PersonalInfo> PersonalInfos { get; set; }
        public DbSet<Certificates> Certificates { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<ResumeSkills> ResumeSkills { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<Languages> Languages { get; set; }
        public DbSet<ResumeLanguages> ResumeLanguages { get; set; }
        public DbSet<Locations> Locations { get; set; }
        public DbSet<ResumeLocations> ResumeLocations { get; set; }
        public DbSet<Templates> Templates { get; set; }
        public DbSet<ResumeTemplates> ResumeTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResumeSkills>()
                .HasKey(e => new { e.ResumeId, e.SkillId });

            modelBuilder.Entity<ResumeSkills>()
            .HasOne(rs => rs.Resume)
            .WithMany(r => r.ResumeSkills)
            .HasForeignKey(rs => rs.ResumeId);

            modelBuilder.Entity<ResumeSkills>()
                .HasOne(rs => rs.Skill)
                .WithMany(s => s.ResumeSkills)
                .HasForeignKey(rs => rs.SkillId);


            modelBuilder.Entity<ResumeLanguages>()
                .HasKey(e => new { e.ResumeId, e.LanguageId });

            modelBuilder.Entity<ResumeLanguages>()
                .HasOne(rl => rl.Resume)
                .WithMany(r => r.ResumeLanguages)
                .HasForeignKey(rl => rl.ResumeId);

            modelBuilder.Entity<ResumeLanguages>()
                .HasOne(rl => rl.Language)
                .WithMany(r => r.ResumeLanguages)
                .HasForeignKey(rl => rl.LanguageId);


            modelBuilder.Entity<ResumeLocations>()
                .HasKey(e => new { e.ResumeId, e.LocationId });

            modelBuilder.Entity<ResumeLocations>()
                .HasOne(rl => rl.Location)
                .WithMany(r => r.ResumeLocations)
                .HasForeignKey(rl => rl.LocationId);

            modelBuilder.Entity<ResumeLocations>()
                .HasOne(rl => rl.Resume)
                .WithMany(r => r.ResumeLocations)
                .HasForeignKey(rl => rl.ResumeId);


            modelBuilder.Entity<ResumeTemplates>()
                .HasKey(e => new { e.ResumeId, e.TemplateId });

            modelBuilder.Entity<ResumeTemplates>()
                .HasOne(rt => rt.Template)
                .WithMany(r => r.ResumeTemplates)
                .HasForeignKey(rt => rt.TemplateId);

            modelBuilder.Entity<ResumeTemplates>()
                .HasOne(rt => rt.Resume)
                .WithMany(r => r.ResumeTemplates)
                .HasForeignKey(rt => rt.ResumeId);


            modelBuilder.Entity<Templates>().HasData
                (
                    new Templates
                    {
                        Id = 1,
                        TemplateName = "CoolTemplate",
                        TemplateFilePath = @"C:\Online CV Builder\Online CV Builder"
                    },
                    new Templates
                    {
                        Id = 2,
                        TemplateName = "VeryCoolTemplate",
                        TemplateFilePath = @"C:\Online CV Builder\Online CV Builder"
                    }
                );

            modelBuilder.Entity<Resumes>().HasData
                (
                        new Resumes { Id = 1, UserId = 1, Title = "John's Resume" },
                        new Resumes { Id = 2, UserId = 2, Title = "Jane's Resume" }
                );
            modelBuilder.Entity<PersonalInfo>().HasData
                (
                    new PersonalInfo { Id = 1, ResumeId = 1, FullName = "John Doe", PhoneNumber = "123456789", Email = "john.doe@example.com" , Address = "none"},
                        new PersonalInfo { Id = 2, ResumeId = 2, FullName = "Jane Smith", PhoneNumber = "987654321", Email = "jane.smith@example.com" , Address ="none"}
                );
            modelBuilder.Entity<WorkExperience>().HasData
                (
                    new WorkExperience { Id = 1, ResumeId = 1, CompanyName = "ABC Corp", Position = "Software Developer", StartDate = new DateTime(2019, 6, 1), EndDate = new DateTime(2021, 12, 31), Description = "Developed web applications using ASP.NET Core" },
                        new WorkExperience { Id = 2, ResumeId = 2, CompanyName = "XYZ Inc", Position = "Project Manager", StartDate = new DateTime(2020, 6, 1), EndDate = new DateTime(2022, 12, 31), Description = "Managed software development projects" }
                );
            modelBuilder.Entity<Languages>().HasData
                (
                    new Languages { Id = 1, LanguageName = "English", ProficiencyLevel = "Fluent" },
                        new Languages { Id = 2, LanguageName = "French", ProficiencyLevel = "Intermediate" }
                );
            modelBuilder.Entity<Skills>().HasData
                (
                    new Skills {Id = 1, SkillName = "C#" },
                        new Skills { Id = 2, SkillName = "JavaScript" },
                        new Skills { Id = 3, SkillName = "Project Management" }
                );
            modelBuilder.Entity<Locations>().HasData
                (
                    new Locations { Id = 1, City = "New York", State = "NY", Country = "USA" },
                        new Locations { Id = 2, City = "Paris", State = "", Country = "France" }
                );
            modelBuilder.Entity<ResumeSkills>().HasData
                (
                    new ResumeSkills { ResumeId = 1, SkillId = 1 },
                        new ResumeSkills { ResumeId = 2, SkillId = 2 },
                        new ResumeSkills { ResumeId = 2, SkillId = 3 }
                );
            modelBuilder.Entity<ResumeLanguages>().HasData
                (
                    new ResumeLanguages { ResumeId = 1, LanguageId = 1 },
                        new ResumeLanguages { ResumeId = 2, LanguageId = 1 },
                        new ResumeLanguages { ResumeId = 2, LanguageId = 2 }
                );
            modelBuilder.Entity<ResumeLocations>().HasData
                (
                    new ResumeLocations { ResumeId = 1, LocationId = 1 },
                        new ResumeLocations { ResumeId = 2, LocationId = 2 }
                );
            modelBuilder.Entity<Education>().HasData
                (
                    new Education { Id = 1, ResumeId = 1, InstituteName = "University of ABC", Degree = "Bachelor's Degree", FieldOfStudy = "Computer Science", StartDate = new DateTime(2015, 9, 1), EndDate = new DateTime(2019, 5, 31) },
                        new Education { Id = 2, ResumeId = 2, InstituteName = "University of XYZ", Degree = "Master's Degree", FieldOfStudy = "Business Administration", StartDate = new DateTime(2018, 9, 1), EndDate = new DateTime(2020, 5, 31) }
                );
            modelBuilder.Entity<Certificates>().HasData
                (
                    new Certificates { Id = 1, ResumeId = 1, CertificateName = "Microsoft Certified Professional", IssuingOrganization = "Microsoft", IssueDate = new DateTime(2020, 1, 15) },
                        new Certificates { Id = 2, ResumeId = 2, CertificateName = "Project Management Professional (PMP)", IssuingOrganization = "PMI", IssueDate = new DateTime(2021, 2, 20) }
                );
            modelBuilder.Entity<ResumeTemplates>().HasData
                (
                    new ResumeTemplates { ResumeId = 1, TemplateId = 1 },
                        new ResumeTemplates { ResumeId = 2, TemplateId = 2 }
                );
        }

       
    }
}
