using System;
using System.IO;
using System.Threading.Tasks;
using csharp.models;
using Microsoft.AspNetCore.Html;

namespace csharp.services.scoped.impl
{
    public class InvoiceService: IInvoiceService
    {
        private readonly IFileService _fileService;
        private readonly IPdfService _pdfService;

        public InvoiceService(IFileService fileService, IPdfService pdfService)
        {
            _fileService = fileService;
            _pdfService = pdfService;
        }
        
        public Task ReconcileInvoiceEvent(string outputDirectory, Event item)
        {
            var fileName = Path.Join(outputDirectory, $"{item.Id}.pdf");
            return item.Type switch
            {
                EventType.InvoiceDeleted => DeleteInvoiceIfExists(item, fileName),
                _ => CreateOrUpdateInvoice(item, fileName),
            };
        }

        private Task CreateOrUpdateInvoice(Event item, string fileName)
        {
            // remove any existing invoice (update means override file so same as create)
            _fileService.DeleteFileIfExists(fileName);
            var invoiceHtml = GetInvoiceContent(item);
            _pdfService.WritePdf(invoiceHtml, fileName);
            Console.WriteLine($"Wrote PDF for {item.Id} to {fileName}");
            return Task.CompletedTask;
        }

        private string GetInvoiceContent(Event item)
        {
            // TODO would use templated razor pages for real project
            var title = $"Invoice {item.Id}";
            return $"<html><head><title>{title}</title></head><body><h1>{title}</h1></body></html>";
        }


        private Task DeleteInvoiceIfExists(Event item, string fileName)
        {
            Console.WriteLine(_fileService.DeleteFileIfExists(fileName)
                ? $"Invoice deleted {item.Id}"
                : $"Invoice doesn't exist to delete {item.Id}");
            return Task.CompletedTask;
        }
    }
}