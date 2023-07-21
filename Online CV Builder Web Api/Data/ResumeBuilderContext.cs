using Microsoft.EntityFrameworkCore;
using Online_CV_Builder.Data.Entities;

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



          
        }

    }
}
