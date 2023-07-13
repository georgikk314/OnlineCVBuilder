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

            string username = "georgikk";

            var folderPath = Path.Combine("PathToRootFolder", username, $"resume{resumeId}");

            // Create the user folder if it doesn't exist
            if (!Directory.Exists(username))
            {
                Directory.CreateDirectory(username);
            }

            // Create the resume folder if it doesn't exist
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePath = Path.Combine(folderPath, "resume.pdf");

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

        private string ConstructHtmlContent(Templates template, Resumes resume)
        {
            var templatePath = $"PathToTemplatesFolder/Template{template.Id}/Template{template.Id}.cshtml";
            var templateHtml = File.ReadAllText(templatePath);

            // Replace the placeholders in the template HTML with resume data
            var htmlContent = templateHtml;
                

            return htmlContent;
        }

        private void GeneratePdfFromHtml(string htmlContent, string filePath)
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
    }
}
