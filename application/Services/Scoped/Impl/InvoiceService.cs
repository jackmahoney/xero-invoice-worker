using System.IO;
using System.Threading.Tasks;
using csharp.models;
using Microsoft.Extensions.Logging;

namespace csharp.services.scoped.impl
{
    public class InvoiceService: IInvoiceService
    {
        private readonly IFileService _fileService;
        private readonly IPdfService _pdfService;
        private readonly ILogger<InvoiceService> _logger;

        public InvoiceService(IFileService fileService, IPdfService pdfService, ILogger<InvoiceService> logger)
        {
            _fileService = fileService;
            _pdfService = pdfService;
            _logger = logger;
        }
        /**
         * Given an event item reconcile invoice PDFs in output directory
         */
        public Task ReconcileInvoiceEvent(string outputDirectory, Event item)
        {
            var fileName = Path.Join(outputDirectory, $"{item.Id}.pdf");
            // act based on event type
            return item.Type switch
            {
                EventType.INVOICE_DELETED => DeleteInvoiceIfExists(item, fileName),
                _ => CreateOrUpdateInvoice(item, fileName),
            };
        }

        private Task CreateOrUpdateInvoice(Event item, string fileName)
        {
            // remove any existing invoice (update means override file so same as create)
            _fileService.DeleteFileIfExists(fileName);
            
            // create html for the invoice and write to disk
            var invoiceHtml = GenerateInvoiceContent(item);
            _pdfService.WritePdf(invoiceHtml, fileName);
            _logger.LogInformation($"Wrote PDF for {item.Id} to {fileName}");
            
            return Task.CompletedTask;
        }

        /**
         * Generate HTML for an invoice PDF from a given event
         */
        private string GenerateInvoiceContent(Event item)
        {
            // In real project would use templated razor pages for more maintainable and flexible content
            var title = $"Invoice {item.Id}";
            return $"<html><head><title>{title}</title></head><body><h1>{title}</h1></body></html>";
        }


        private Task DeleteInvoiceIfExists(Event item, string fileName)
        {
            // Delete an invoice if it exists
            if (_fileService.DeleteFileIfExists(fileName))
            {
                _logger.LogInformation($"Invoice deleted {item.Id}");
            }
            else
            {
                 _logger.LogWarning($"Invoice doesn't exist to delete {item.Id}");
            }
            return Task.CompletedTask;
        }
    }
}