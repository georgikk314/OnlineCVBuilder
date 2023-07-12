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
                });

            modelBuilder.Entity("Online_CV_Builder.Data.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Expiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
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

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefreshTokenExpiration")
                        .HasColumnType("datetime2");

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
                });

            modelBuilder.Entity("Online_CV_Builder.Data.Entities.RefreshToken", b =>
                {
                    b.HasOne("Online_CV_Builder.Data.Entities.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
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
