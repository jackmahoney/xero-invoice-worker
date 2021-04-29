using Application.Models;
using Application.Services.Scoped;
using Application.Services.Scoped.Impl;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace test.Services
{
    public class InvoiceServiceTest
    {
        private const string OutputDirectory = "/tmp/out";

        [Fact]
        public async void CanProcess_InvoiceCreated()
        {

            const string fileName = "/tmp/out/invoice-123.pdf";
            const string htmlContent = "<html></html>";
            
            var invoiceCreated = new Event
            {
                Type = EventType.INVOICE_CREATED,
                Content = new EventContent
                {
                    Status = InvoiceStatus.PAID,
                    InvoiceId = "invoice-123",
                    InvoiceNumber = "012378236712"
                }
            };

            var fileService = new Mock<IFileService>();
            var pdfService = new Mock<IPdfService>();
            var logger = new Mock<ILogger<InvoiceService>>();
            var templatingService = new Mock<ITemplatingService>();
            templatingService.Setup(x => x.GenerateInvoiceContent(invoiceCreated.Content)).Returns(htmlContent);

            var service = new InvoiceService(fileService.Object, pdfService.Object, logger.Object,
                templatingService.Object);

            // call
            var path = await service.ReconcileInvoiceEvent(OutputDirectory, invoiceCreated);
            
            // verify pdf written
            pdfService.Verify(x => x.WritePdf(htmlContent, fileName), Times.Once());
            fileService.Verify(x => x.DeleteFileIfExists(fileName), Times.Once());
            
            Assert.Equal(fileName, path);
        }
    }
}