using Application.Services.Scoped;
using Microsoft.Extensions.Logging;
using PdfSharpCore;
using VetCV.HtmlRendererCore.PdfSharpCore;

namespace Application.Services.Scoped.Impl
{
    public class PdfService : IPdfService
    {

        private readonly ILogger<PdfService> _logger;

        public PdfService(ILogger<PdfService> logger)
        {
            _logger = logger;
        }

        public void WritePdf(string html, string path)
        {
            // generate a pdf and save
            var pdf = PdfGenerator.GeneratePdf(html, PageSize.A4);
            pdf.Save(path);
        }
    }
}