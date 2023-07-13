namespace Online_CV_Builder.Services
{
    public interface ITemplateDownloadService
    {
        public Task PDFDownloader(string filePath);
    }
}
