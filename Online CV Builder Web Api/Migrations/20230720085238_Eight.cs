using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_CV_Builder.Migrations
{
    public partial class Eight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Certificates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Certificates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Education",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Education",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PersonalInfos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PersonalInfos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ResumeLanguages",
                keyColumns: new[] { "LanguageId", "ResumeId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ResumeLanguages",
                keyColumns: new[] { "LanguageId", "ResumeId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ResumeLanguages",
                keyColumns: new[] { "LanguageId", "ResumeId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "ResumeLocations",
                keyColumns: new[] { "LocationId", "ResumeId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ResumeLocations",
                keyColumns: new[] { "LocationId", "ResumeId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "ResumeSkills",
                keyColumns: new[] { "ResumeId", "SkillId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ResumeSkills",
                keyColumns: new[] { "ResumeId", "SkillId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "ResumeSkills",
                keyColumns: new[] { "ResumeId", "SkillId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "ResumeTemplates",
                keyColumns: new[] { "ResumeId", "TemplateId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ResumeTemplates",
                keyColumns: new[] { "ResumeId", "TemplateId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "WorkExperiences",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WorkExperiences",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Resumes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Resumes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Templates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Templates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ResumeTemplates",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "ResumeTemplates");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Certificates",
                columns: new[] { "Id", "CertificateName", "IssueDate", "IssuingOrganization", "ResumeId" },
                values: new object[,]
                {
                    { 1, "Microsoft Certified Professional", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Microsoft", 1 },
                    { 2, "Project Management Professional (PMP)", new DateTime(2021, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "PMI", 2 }
                });

            migrationBuilder.InsertData(
                table: "Education",
                columns: new[] { "Id", "Degree", "EndDate", "FieldOfStudy", "InstituteName", "ResumeId", "StartDate" },
                values: new object[,]
                {
                    { 1, "Bachelor's Degree", new DateTime(2019, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Computer Science", "University of ABC", 1, new DateTime(2015, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Master's Degree", new DateTime(2020, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Business Administration", "University of XYZ", 2, new DateTime(2018, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "LanguageName", "ProficiencyLevel" },
                values: new object[,]
                {
                    { 1, "English", "Fluent" },
                    { 2, "French", "Intermediate" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "City", "Country", "State" },
                values: new object[,]
                {
                    { 1, "New York", "USA", "NY" },
                    { 2, "Paris", "France", "" }
                });

            migrationBuilder.InsertData(
                table: "PersonalInfos",
                columns: new[] { "Id", "Address", "Email", "FullName", "PhoneNumber", "ResumeId" },
                values: new object[,]
                {
                    { 1, "none", "john.doe@example.com", "John Doe", "123456789", 1 },
                    { 2, "none", "jane.smith@example.com", "Jane Smith", "987654321", 2 }
                });

            migrationBuilder.InsertData(
                table: "Resumes",
                columns: new[] { "Id", "CreationDate", "LastModifiedDate", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "John's Resume", 1 },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane's Resume", 2 }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "SkillName" },
                values: new object[,]
                {
                    { 1, "C#" },
                    { 2, "JavaScript" },
                    { 3, "Project Management" }
                });

            migrationBuilder.InsertData(
                table: "Templates",
                columns: new[] { "Id", "TemplateFilePath", "TemplateName" },
                values: new object[,]
                {
                    { 1, "C:\\Online CV Builder\\Online CV Builder", "CoolTemplate" },
                    { 2, "C:\\Online CV Builder\\Online CV Builder", "VeryCoolTemplate" }
                });

            migrationBuilder.InsertData(
                table: "WorkExperiences",
                columns: new[] { "Id", "CompanyName", "Description", "EndDate", "Position", "ResumeId", "StartDate" },
                values: new object[,]
                {
                    { 1, "ABC Corp", "Developed web applications using ASP.NET Core", new DateTime(2021, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Software Developer", 1, new DateTime(2019, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "XYZ Inc", "Managed software development projects", new DateTime(2022, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Project Manager", 2, new DateTime(2020, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ResumeLanguages",
                columns: new[] { "LanguageId", "ResumeId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "ResumeLocations",
                columns: new[] { "LocationId", "ResumeId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "ResumeSkills",
                columns: new[] { "ResumeId", "SkillId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "ResumeTemplates",
                columns: new[] { "ResumeId", "TemplateId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });
        }
    }
}
