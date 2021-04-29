using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.Scoped;
using Microsoft.Extensions.Logging;

namespace Application.Services.Scoped.Impl
{
    public class Runner : IRunner
    {
        private readonly IRequestService _requestService;
        private readonly IEventStoreService _eventStoreService;
        private readonly IInvoiceService _invoiceService;
        private readonly ILogger<Runner> _logger;

        public Runner(IRequestService requestService, IEventStoreService eventStoreService, IInvoiceService invoiceService, ILogger<Runner> logger)
        {
            _requestService = requestService;
            _eventStoreService = eventStoreService;
            _invoiceService = invoiceService;
            _logger = logger;
        }

        public async Task<long?> Process(Uri inputUrl, string outputDir, int pageSize, long? lastId)
        {
            _logger.LogInformation($"Polling event feed");
            try
            {
                // call the feed url and deserialize event response
                var eventsResponse = await _requestService.GetEvents(inputUrl, pageSize, lastId);


                _logger.LogInformation(eventsResponse.ToString());
                var items = eventsResponse.Items;
                _logger.LogInformation($"Received {items.Count} events");

                // filter out seen events to leave only new events
                var newItems = await _eventStoreService.SelectNewEvents(eventsResponse.Items);
                _logger.LogInformation($"New count {newItems.Count}");

                // reconcile events with filesystem
                await Task.WhenAll(newItems.Select(item => _invoiceService.ReconcileInvoiceEvent(outputDir, item)).ToList());

                // persist processed events
                var inserted = await _eventStoreService.PersistProcessedEvents(newItems);
                _logger.LogInformation($"Persisted {inserted.Count} records");

                // return id of last processed
                // here we assume feed url returns results ordered by ascending ID
                return items.Select(i => i.Id).LastOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"Processing exception: {e}");
                return null;
            }
        }
    }
}