using System;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Models;
using Application.Services.Scoped;
using Application.Services.Scoped.Impl;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace test.Services
{
    public class RequestServiceTest
    {
        private const string InboxCreated = @"
{
  ""items"": [
    {
        ""id"": 1,
        ""type"": ""INVOICE_CREATED"",
        ""content"": {
            ""invoiceId"": ""invoice-1"",
            ""invoiceNumber"": ""test-123"",
            ""lineItems"": [
                {
                    ""lineItemId"": ""123"",
                    ""description"": ""description"",
                    ""quantity"": 9.5,
                    ""unitCost"": 50,
                    ""lineItemTotalCost"": 475
                }
            ],
            ""status"": ""PAID"",
            ""dueDateUtc"": ""2021-04-28T23:06:25.438Z"",
            ""createdDateUtc"": ""2021-04-28T23:06:25.438Z"",
            ""updatedDateUtc"": ""2021-04-28T23:06:25.438Z""
        },
        ""createdDateUtc"": ""2021-04-28T23:06:25.438Z""
    }
  ]
}
";

        private const string Endpoint = "https://localhost:3000/events";

        [Fact]
        public async void CanCallEndpoint_WithValidResponse()
        {
            var date = DateTime.Parse("2021-04-28T23:06:25.438Z", null, DateTimeStyles.RoundtripKind);
            
            // mock fields and http response
            var logger = new Mock<ILogger<RequestService>>();
            var httpService = new Mock<IHttpService>();
            httpService.Setup(client => client.GetStringAsync(It.IsAny<string>())).Returns(Task.FromResult(InboxCreated));
            
            // create
            var requestService = new RequestService(httpService.Object, logger.Object);

            // call with since param
            var response = await requestService.GetEvents(new Uri(Endpoint), 10, 123);
            httpService.Verify(client => client.GetStringAsync($"{Endpoint}?pageSize=10&afterEventId=123"),
                Times.Once());
            
            // get items from response
            Assert.Single(response.Items);
            var first = response.Items.First();

            // test deserialization is correct
            Assert.Equal(1, first.Id);
            Assert.Equal(EventType.INVOICE_CREATED, first.Type);
            Assert.Equal("invoice-1", first.Content.InvoiceId);
            Assert.Equal("test-123", first.Content.InvoiceNumber);
            Assert.Equal(InvoiceStatus.PAID, first.Content.Status);
            Assert.Equal(date, first.Content.DueDateUtc);
            Assert.Equal(date, first.Content.CreatedDateUtc);
            Assert.Equal(date, first.Content.UpdatedDateUtc);
            Assert.Equal(date, first.CreatedDateUtc);

            // call without param
            await requestService.GetEvents(new Uri(Endpoint), 15, null);
            httpService.Verify(client => client.GetStringAsync($"{Endpoint}?pageSize=15"), Times.Once());
        }
        
    [Fact]
        public async void CanHandleBadResponses()
        {
            
            var logger = new Mock<ILogger<RequestService>>();
            var httpService = new Mock<IHttpService>();
            httpService.Setup(client => client.GetStringAsync(It.IsAny<string>())).Returns(Task.FromResult("bad json"));
            
            // create and call expecting json exception
            var requestService = new RequestService(httpService.Object, logger.Object);
            await Assert.ThrowsAsync<JsonException>(() => requestService.GetEvents(new Uri(Endpoint), 10, null));
        }
    }
}