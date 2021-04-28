using System;
using System.Linq;
using System.Threading.Tasks;
using csharp.models;

namespace csharp.services.scoped.impl
{
    public class Runner : IRunner
    {
        private readonly IRequestService _requestService;
        private readonly IEventStoreService _eventStoreService;
        private readonly IInvoiceService _invoiceService;

        public Runner(IRequestService requestService, IEventStoreService eventStoreService, IInvoiceService invoiceService)
        {
            _requestService = requestService;
            _eventStoreService = eventStoreService;
            _invoiceService = invoiceService;
        }

        public async Task<int?> Process(Uri inputUrl, string outputDir, int? lastId)
        {
            Console.WriteLine($"Polling {inputUrl}");
            try
            {
                // call the feed url and deserialize event response
                var eventsResponse = await _requestService.GetEvents(inputUrl, 10, lastId);
                var items = eventsResponse.Items;
                Console.WriteLine($"Received {items.Count} events");
                
                // filter out seen events to leave only new events
                var newItems = await _eventStoreService.SelectNewEvents(eventsResponse.Items);
                Console.WriteLine($"New count {newItems.Count}");
                
                // reconcile events with filesystem
                await Task.WhenAll(newItems.Select(item => _invoiceService.ReconcileInvoiceEvent(outputDir, item)).ToList());

                // persist processed events
                var inserted = await _eventStoreService.PersistProcessedEvents(newItems);
                Console.WriteLine($"Persisted {inserted} records");
                
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }
    }
}