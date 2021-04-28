using Microsoft.Extensions.Logging;
using PdfSharpCore;
using VetCV.HtmlRendererCore.PdfSharpCore;

namespace csharp.services.scoped.impl
{
    public class PdfService: IPdfService
    {

        private readonly ILogger<PdfService> _logger;

        public PdfService(ILogger<PdfService> logger)
        {
            _logger = logger;
        }

        public void WritePdf(string html, string path)
        {
            // generate a pdf
            _logger.LogInformation("Generate PDF");
            var pdf = PdfGenerator.GeneratePdf(html, PageSize.A4);
         
            // write the file
            _logger.LogInformation($"Save PDF to {path}");
            pdf.Save(path);
        }
    }
}