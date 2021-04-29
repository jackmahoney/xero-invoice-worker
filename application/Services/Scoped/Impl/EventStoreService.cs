using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.db;
using Application.Models;
using Application.Services.Scoped;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services.Scoped.Impl
{
    /**
     * Filter event items against database of processed records
     */
    public class EventStoreService : IEventStoreService
    {
        private readonly EventRecordContext _context;
        private readonly ILogger<EventStoreService> _logger;

        public EventStoreService(EventRecordContext context, ILogger<EventStoreService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /**
         * Filter list of event items and remove those that exist in the event record database
         * Assumes that event feed produces events with ID field that denotes identity
         */
        public async Task<List<Event>> SelectNewEvents(List<Event> events)
        {
            var count = await _context.EventRecords.LongCountAsync();
            var eventIds = events.Select(e => e.Id);
            var existingRecords = await _context.EventRecords.Where(r => eventIds.Contains(r.EventId)).ToListAsync();
            _logger.LogInformation($"Filter {events.Count} - found {existingRecords.Count} out of {count} event records");
            var existingIds = existingRecords.Select(r => r.EventId);
            var newEvents = events.Where(e => existingIds.Contains(e.Id) == false).ToList();
            _logger.LogInformation($"{newEvents.Count} new events");
            return newEvents;
        }

        /**
         * Save processed events as event records to prevent future processing
         * Assumes passed events do not have records already matching their IDs (so filter before call)
         */
        public async Task<List<long>> PersistProcessedEvents(List<Event> events)
        {
            // create new record for each event
            var records = events.Select(e =>
            {
                var record = new EventRecord { EventId = e.Id, Hash = $"{e.Id}{e.Type}", CreatedAt = DateTime.Now };
                _logger.LogInformation($"Persisting event id {record.EventId}");
                return record;
            }).ToList();
            var ids = records.Select(r => r.EventId).ToList();
            _logger.LogInformation($"Save changes {records.Count}");
            _context.EventRecords.AddRange(records);
            await _context.SaveChangesAsync();
            return ids;
        }
    }
}