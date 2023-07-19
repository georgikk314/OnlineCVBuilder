using IronPdf.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_CV_Builder.Data;
using Online_CV_Builder.Data.Entities;
using System.Net;
using System.Net.Http.Headers;

namespace Online_CV_Builder.Services
{
    public class TemplateDownloadService : ITemplateDownloadService
    {
        private readonly ResumeBuilderContext _dbContext;
        public TemplateDownloadService(ResumeBuilderContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string ContructionOfTemplate(int resumeId)
        {
 
            // Get the resume and user information based on the resumeId
            var resume = _dbContext.Resumes.FirstOrDefault(r => r.Id == resumeId);
            
            if (resume == null)
            {
                throw new ArgumentException("Invalid resumeId.");
            }

            string username = _dbContext.Users.FirstOrDefault(u => u.Id == resume.UserId).Username;

            var folderPath = Path.Combine("UsersTemplates", username, $"resume{resumeId}");


            // Create the resume folder if it doesn't exist
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePath = Path.Combine(folderPath, $"{resume.Title}.pdf");

            var templateId = _dbContext.ResumeTemplates.FirstOrDefault(rt => rt.ResumeId == resumeId).TemplateId;
            var template = _dbContext.Templates.FirstOrDefault(t => t.Id == templateId);
            if (template == null)
            {
                throw new ArgumentException("Invalid templateId.");
            }

            var htmlContent = ConstructHtmlContent(template, resume);

            // Generate the PDF file
            GeneratePdfFromHtml(htmlContent, filePath);

            return filePath;

        }

        public string ConstructHtmlContent(Templates template, Resumes resume)
        {
            var templatePath = $"C:/Users/PC/source/repos/georgikk314/OnlineCVBuilder/Templates/Template{template.Id}/Template{template.Id}.cshtml";
            var templateHtml = File.ReadAllText(templatePath);

            // Replace the placeholders in the template HTML with resume data
            var personalInfo = _dbContext.PersonalInfos.FirstOrDefault(p => p.ResumeId == resume.Id);
            var htmlContent = templateHtml;

            // Personal Information
            htmlContent = htmlContent.Replace("<!-- Title -->", resume.Title);
            htmlContent = htmlContent.Replace("<!-- FullName -->", personalInfo.FullName);
            htmlContent = htmlContent.Replace("<!-- PhoneNumber -->", personalInfo.PhoneNumber);
            htmlContent = htmlContent.Replace("<!-- Email -->", personalInfo.Email);

            // Locations
            var resumeLocations = _dbContext.ResumeLocations.Where(rl => rl.ResumeId == resume.Id).ToList();
            string locations = "";
            foreach (var resumeLocation in resumeLocations)
            {
                var location = _dbContext.Locations.FirstOrDefault(l => l.Id == resumeLocation.LocationId);
                locations += $"<span class=\"text\"> {location.Country}, {location.City}, {location.State} </span>";
            }
            htmlContent = htmlContent.Replace("<!-- Locations -->", locations);

            // Education
            var educations = _dbContext.Education.Where(e => e.ResumeId == resume.Id).OrderBy(e => e.StartDate).ToList();
            string educationHtmlString = "";
            foreach (var education in educations)
            {
                educationHtmlString += $"<li><h5>From {education.StartDate.Value.Month}/{education.StartDate.Value.Year} to {education.EndDate.Value.Month}/{education.EndDate.Value.Year}</h5><h4>I studied at {education.InstituteName}</h4><h4>I have a {education.Degree} in {education.FieldOfStudy}</h4></li>";
            }
            htmlContent = htmlContent.Replace("<!-- Educations -->", educationHtmlString);

            // Languages
            var resumeLanguages = _dbContext.ResumeLanguages.Where(rl => rl.ResumeId == resume.Id).ToList();
            string languagesHtmlString = "";
            foreach (var resumeLanguage in resumeLanguages)
            {
                var language = _dbContext.Languages.FirstOrDefault(l => l.Id == resumeLanguage.LanguageId);
                languagesHtmlString += $"<li><span class=\"text\">{language.LanguageName} - {language.ProficiencyLevel}</span></li>";
         
            }
            htmlContent = htmlContent.Replace("<!-- Languages -->", languagesHtmlString);

            // Work Experience
            var workExperiences = _dbContext.WorkExperiences.Where(e => e.ResumeId == resume.Id).OrderBy(e => e.StartDate).ToList();
            string workExperienceHtmlString = "";
            foreach(var workExperience in workExperiences)
            {
                workExperienceHtmlString += $"<p>From {workExperience.StartDate.Value.Month}/{workExperience.StartDate.Value.Year} to {workExperience.EndDate.Value.Month}/{workExperience.EndDate.Value.Year}<br>I worked at {workExperience.CompanyName} as {workExperience.Position}<br>{workExperience.Description}</p>";
            }
            htmlContent = htmlContent.Replace("<!-- Work Experiences -->", workExperienceHtmlString);

            // Skills
            var resumeSkills = _dbContext.ResumeSkills.Where(s => s.ResumeId == resume.Id).ToList();
            string skillsHtmlString = "";
            foreach (var resumeSkill in resumeSkills)
            {
                var skill = _dbContext.Skills.FirstOrDefault(s => s.Id == resumeSkill.SkillId);
                skillsHtmlString += $"<p>{skill.SkillName}</p>";
            }
            htmlContent = htmlContent.Replace("<!-- Skills -->", skillsHtmlString);

            // Certificates
            var certificates = _dbContext.Certificates.Where(c => c.ResumeId == resume.Id).OrderBy(c => c.IssueDate).ToList();
            string certificatesHtmlString = "";
            foreach (var certificate in certificates)
            {
                certificatesHtmlString += $"<p>{certificate.CertificateName}<br>Issued by {certificate.IssuingOrganization}<br>On {certificate.IssueDate.Value.Month}/{certificate.IssueDate.Value.Year}</p>";
            }
            htmlContent = htmlContent.Replace("<!-- Certificates -->", certificatesHtmlString);

            return htmlContent;
        }

        public PdfDocument GeneratePdfFromHtml(string htmlContent, string filePath)
        {
            // Create a new IronPdf.HtmlToPdf object
            var renderer = new IronPdf.HtmlToPdf();

            // Optionally set the PDF generation options
            renderer.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.A4;
            renderer.PrintOptions.MarginTop = 10;
            renderer.PrintOptions.MarginBottom = 10;
            renderer.PrintOptions.MarginLeft = 10;
            renderer.PrintOptions.MarginRight = 10;

            // Generate the PDF from the HTML content
            var pdfDocument = renderer.RenderHtmlAsPdf(htmlContent);

            // Save the PDF to the specified file path
            pdfDocument.SaveAs(filePath);

            //Return the PDF file
            return pdfDocument;
        }

        public async Task PDFDownloader(string filePath)
        {

            byte[] fileBytes = File.ReadAllBytes(filePath);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(fileBytes)
            };

            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = System.IO.Path.GetFileName(filePath)
            };

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            var bytes = await response.Content.ReadAsByteArrayAsync();

            // Save the file to the Downloads folder
            string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string downloadedFilePath = System.IO.Path.Combine(downloadsPath, System.IO.Path.GetFileName(filePath));
            File.WriteAllBytes(downloadedFilePath, bytes);
        }

        public void DeletePdfTemplate(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                else
                {
                    throw new FileNotFoundException("The PDF file does not exist.");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions thrown during file deletion
                Console.WriteLine($"Error deleting PDF file: {ex.Message}");
                throw;
            }
        }
    }
}
