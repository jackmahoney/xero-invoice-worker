using System;
using System.Threading.Tasks;
using csharp.models;

namespace csharp.services.scoped.impl
{
    public class Runner : IRunner
    {
        private readonly IRequestService _requestService;

        public Runner(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task Process(Uri inputUrl, string outputDir)
        {
            Console.WriteLine($"Polling {inputUrl}");
            try
            {
                var events = await _requestService.GetEvents(inputUrl);
                foreach (var eventItem in events.Items)
                {
                    LogEvent(eventItem);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }

        private void LogEvent(Event eventItem)
        {
            if (eventItem.Type == EventType.InvoiceDeleted)
            {
                var e = eventItem.content as InvoiceDeletedEventContent;
                Console.WriteLine("Delete"); 
            }
            else
            {
                var e = eventItem.content as InvoiceCreatedOrUpdatedEventContent;
                Console.WriteLine("Create or update"); 
            }
        }
    }
}