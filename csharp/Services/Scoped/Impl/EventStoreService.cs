using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using csharp.db;
using csharp.models;
using Microsoft.EntityFrameworkCore;

namespace csharp.services.scoped.impl
{
    /**
     * Filter event items against database of processed records
     */
    public class EventStoreService: IEventStoreService
    {
        private readonly EventRecordContext _context;

        public EventStoreService(EventRecordContext context)
        {
            _context = context;
        }
        
        /**
         * Filter list of event items and remove those that exist in the event record database
         * Assumes that event feed produces events with ID field that denotes identity
         */
        public async Task<List<Event>> SelectNewEvents(List<Event> events)
        {
            var eventIds = events.Select(e => e.Id);
            // TODO use SHA to hash the content too
            var existingRecords = await _context.EventRecords.Where(r => eventIds.Contains(r.Id)).ToListAsync();
            var existingIds = existingRecords.Select(r => r.Id);
            return events.Where(e => existingIds.Contains(e.Id) == false).ToList();
        }

        /**
         * Save processed events as event records to prevent future processing
         * Assumes passed events do not have records already matching their IDs (so filter before call)
         */
        public async Task<List<EventRecord>> PersistProcessedEvents(List<Event> events)
        {
            // create new record for each event
            var records = events.Select(e => new EventRecord{Id = e.Id, Hash = $"{e.Id}{e.Type}", CreatedAt = DateTime.Now}).ToList();
            await _context.EventRecords.AddRangeAsync(records);
            await _context.SaveChangesAsync();
            return records;
        }
    }
}