using System.IO;
using System.Threading.Tasks;
using Application.Models;
using Application.Services.Scoped;
using Microsoft.Extensions.Logging;

namespace Application.Services.Scoped.Impl
{
    public class InvoiceService: IInvoiceService
    {
        private readonly IFileService _fileService;
        private readonly IPdfService _pdfService;
        private readonly ILogger<InvoiceService> _logger;
        private readonly ITemplatingService _templatingService;

        public InvoiceService(IFileService fileService, IPdfService pdfService, ILogger<InvoiceService> logger, ITemplatingService templatingService)
        {
            _fileService = fileService;
            _pdfService = pdfService;
            _logger = logger;
            _templatingService = templatingService;
        }
        /**
         * Given an event item reconcile invoice PDFs in output directory
         */
        public Task<string> ReconcileInvoiceEvent(string outputDirectory, Event item)
        {
            var fileName = Path.Join(outputDirectory, $"{item.Content.InvoiceId}.pdf");
            // act based on event type
            return item.Type switch
            {
                EventType.INVOICE_DELETED => DeleteInvoiceIfExists(item, fileName),
                _ => CreateOrUpdateInvoice(item, fileName),
            };
        }

        private Task<string> CreateOrUpdateInvoice(Event item, string fileName)
        {
            // remove any existing invoice (update means override file so same as create)
            _fileService.DeleteFileIfExists(fileName);
            
            // create html for the invoice and write to disk
            var invoiceHtml = _templatingService.GenerateInvoiceContent(item.Content);
            _pdfService.WritePdf(invoiceHtml, fileName);
            _logger.LogInformation($"Wrote PDF for {item.Id} to {fileName}");
            
            return Task.FromResult(fileName);
        }

        private Task<string> DeleteInvoiceIfExists(Event item, string fileName)
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
            return Task.FromResult(fileName);
        }
    }
}