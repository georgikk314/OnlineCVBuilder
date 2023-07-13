using Online_CV_Builder.Data.Entities;

namespace Online_CV_Builder.Services
{
    public interface ITemplateDownloadService
    {
        public Task PDFDownloader(string filePath);
        public void GeneratePdfFromHtml(string htmlContent, string filePath);
        public string ContructionOfTemplate(int resumeId);
        public string ConstructHtmlContent(Templates template, Resumes resume);
    }
}
