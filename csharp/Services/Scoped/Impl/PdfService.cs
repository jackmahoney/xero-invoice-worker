using PdfSharpCore;
using VetCV.HtmlRendererCore.PdfSharpCore;

namespace csharp.services.scoped.impl
{
    public class PdfService: IPdfService
    {
        public void WritePdf(string html, string path)
        {
            var pdf = PdfGenerator.GeneratePdf(html, PageSize.A4);
            pdf.Save(path);
        }
    }
}