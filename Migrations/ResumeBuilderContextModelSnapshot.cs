﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Online_CV_Builder.Data;

#nullable disable

namespace Online_CV_Builder.Migrations
{
    [DbContext(typeof(ResumeBuilderContext))]
    partial class ResumeBuilderContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Online_CV_Builder.Data.Entities.Certificates", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CertificateName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IssuingOrganization")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ResumeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Certificates");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CertificateName = "Microsoft Certified Professional",
                            IssueDate = new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IssuingOrganization = "Microsoft",
                            ResumeId = 1
                        },
                        new
                        {
                            Id = 2,
                            CertificateName = "Project Management Professional (PMP)",
                            IssueDate = new DateTime(2021, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IssuingOrganization = "PMI",
                            ResumeId = 2
                        });
                });

            modelBuilder.Entity("Online_CV_Builder.Data.Entities.Education", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Degree")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FieldOfStudy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstituteName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ResumeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Education");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Degree = "Bachelor's Degree",
                            EndDate = new DateTime(2019, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FieldOfStudy = "Computer Science",
                            InstituteName = "University of ABC",
                            ResumeId = 1,
                            StartDate = new DateTime(2015, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Degree = "Master's Degree",
                            EndDate = new DateTime(2020, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FieldOfStudy = "Business Administration",
                            InstituteName = "University of XYZ",
                            ResumeId = 2,
                            StartDate = new DateTime(2018, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Online_CV_Builder.Data.Entities.Languages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("LanguageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProficiencyLevel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Languages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            LanguageName = "English",
                            ProficiencyLevel = "Fluent"
                        },
                        new
                        {
                            Id = 2,
                            LanguageName = "French",
                            ProficiencyLevel = "Intermediate"
                        });
                });

            modelBuilder.Entity("Online_CV_Builder.Data.Entities.Locations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Locations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "New York",
                            Country = "USA",
                            State = "NY"
                        },
                        new
                        {
                            Id = 2,
                            City = "Paris",
                            Country = "France",
                            State = ""
                        });
                });

            modelBuilder.Entity("Online_CV_Builder.Data.Entities.PersonalInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ResumeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PersonalInfos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "none",
                            Email = "john.doe@example.com",
                            FullName = "John Doe",
                            PhoneNumber = "123456789",
                            ResumeId = 1
                        },
                        new
                        {
                            Id = 2,
                            Address = "none",
                            Email = "jane.smith@example.com",
                            FullName = "Jane Smith",
                            PhoneNumber = "987654321",
                            ResumeId = 2
                        });
                });

            modelBuilder.Entity("Online_CV_Builder.Data.Entities.ResumeLanguages", b =>
                {
                    b.Property<int>("ResumeId")
                        .HasColumnType("int");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.HasKey("ResumeId", "LanguageId");

                    b.HasIndex("LanguageId");

                    b.ToTable("ResumeLanguages");

                    b.HasData(
                        new
                        {
                            ResumeId = 1,
                            LanguageId = 1
                        },
                        new
                        {
                            ResumeId = 2,
                            LanguageId = 1
                        },
                        new
                        {
                            ResumeId = 2,
                            LanguageId = 2
                        });
                });

            modelBuilder.Entity("Online_CV_Builder.Data.Entities.ResumeLocations", b =>
                {
                    b.Property<int>("ResumeId")
                        .HasColumnType("int");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.HasKey("ResumeId", "LocationId");

                    b.HasIndex("LocationId");

                    b.ToTable("ResumeLocations");

                    b.HasData(
                        new
                        {
                            ResumeId = 1,
                            LocationId = 1
                        },
                        new
                        {
                            ResumeId = 2,
                            LocationId = 2
                        });
                });

            modelBuilder.Entity("Online_CV_Builder.Data.Entities.Resumes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Resumes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "John's Resume",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            CreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Jane's Resume",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("Online_CV_Builder.Data.Entities.ResumeSkills", b =>
                {
                    b.Property<int>("ResumeId")
                        .HasColumnType("int");

                    b.Property<int>("SkillId")
                        .HasColumnType("int");

                    b.HasKey("ResumeId", "SkillId");

                    b.HasIndex("SkillId");

                    b.ToTable("ResumeSkills");

                    b.HasData(
                        new
                        {
                            ResumeId = 1,
                            SkillId = 1
                        },
                        new
                        {
                            ResumeId = 2,
                            SkillId = 2
                        },
                        new
                        {
                            ResumeId = 2,
                            SkillId = 3
                        });
                });

            modelBuilder.Entity("Online_CV_Builder.Data.Entities.ResumeTemplates", b =>
                {
                    b.Property<int>("ResumeId")
                        .HasColumnType("int");

                    b.Property<int>("TemplateId")
                        .HasColumnType("int");

                    b.HasKey("ResumeId", "TemplateId");

                    b.HasIndex("TemplateId");

                    b.ToTable("ResumeTemplates");

                    b.HasData(
                        new
                        {
                            ResumeId = 1,
                            TemplateId = 1
                        },
                        new
                        {
                            ResumeId = 2,
                            TemplateId = 2
                        });
                });

            modelBuilder.Entity("Online_CV_Builder.Data.Entities.Skills", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("SkillName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Skills");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            SkillName = "C#"
                        },
                        new
                        {
                            Id = 2,
                            SkillName = "JavaScript"
                        },
                        new
                        {
                            Id = 3,
                            SkillName = "Project Management"
                        });
                });

            modelBuilder.Entity("Online_CV_Builder.Data.Entities.Templates", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("TemplateFilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TemplateName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Templates");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            TemplateFilePath = "C:\\Online CV Builder\\Online CV Builder",
                            TemplateName = "CoolTemplate"
                        },
                        new
                        {
                            Id = 2,
                            TemplateFilePath = "C:\\Online CV Builder\\Online CV Builder",
                            TemplateName = "VeryCoolTemplate"
                        });
                });

            modelBuilder.Entity("Online_CV_Builder.Data.Entities.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Online_CV_Builder.Data.Entities.WorkExperience", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ResumeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("WorkExperiences");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CompanyName = "ABC Corp",
                            Description = "Developed web applications using ASP.NET Core",
                            EndDate = new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Position = "Software Developer",
                            ResumeId = 1,
                            StartDate = new DateTime(2019, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            CompanyName = "XYZ Inc",
                            Description = "Managed software development projects",
                            EndDate = new DateTime(2022, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Position = "Project Manager",
                            ResumeId = 2,
                            StartDate = new DateTime(2020, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Online_CV_Builder.Data.Entities.ResumeLanguages", b =>
                {
                    b.HasOne("Online_CV_Builder.Data.Entities.Languages", "Language")
                        .WithMany("ResumeLanguages")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Online_CV_Builder.Data.Entities.Resumes", "Resume")
                        .WithMany("ResumeLanguages")
                        .HasForeignKey("ResumeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("Resume");
                });

            modelBuilder.Entity("Online_CV_Builder.Data.Entities.ResumeLocations", b =>
                {
                    b.HasOne("Online_CV_Builder.Data.Entities.Locations", "Location")
                        .WithMany("ResumeLocations")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Online_CV_Builder.Data.Entities.Resumes", "Resume")
                        .WithMany("ResumeLocations")
                        .HasForeignKey("ResumeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Resume");
                });

            modelBuilder.Entity("Online_CV_Builder.Data.Entities.ResumeSkills", b =>
                {
                    b.HasOne("Online_CV_Builder.Data.Entities.Resumes", "Resume")
                        .WithMany("ResumeSkills")
                        .HasForeignKey("ResumeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Online_CV_Builder.Data.Entities.Skills", "Skill")
                        .WithMany("ResumeSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Resume");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("Online_CV_Builder.Data.Entities.ResumeTemplates", b =>
                {
                    b.HasOne("Online_CV_Builder.Data.Entities.Resumes", "Resume")
                        .WithMany("ResumeTemplates")
                        .HasForeignKey("ResumeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Online_CV_Builder.Data.Entities.Templates", "Template")
                        .WithMany("ResumeTemplates")
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Resume");

                    b.Navigation("Template");
                });

            modelBuilder.Entity("Online_CV_Builder.Data.Entities.Languages", b =>
                {
                    b.Navigation("ResumeLanguages");
                });

            modelBuilder.Entity("Online_CV_Builder.Data.Entities.Locations", b =>
                {
                    b.Navigation("ResumeLocations");
                });

            modelBuilder.Entity("Online_CV_Builder.Data.Entities.Resumes", b =>
                {
                    b.Navigation("ResumeLanguages");

                    b.Navigation("ResumeLocations");

                    b.Navigation("ResumeSkills");

                    b.Navigation("ResumeTemplates");
                });

            modelBuilder.Entity("Online_CV_Builder.Data.Entities.Skills", b =>
                {
                    b.Navigation("ResumeSkills");
                });

            modelBuilder.Entity("Online_CV_Builder.Data.Entities.Templates", b =>
                {
                    b.Navigation("ResumeTemplates");
                });
#pragma warning restore 612, 618
        }
    }
}
