using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models;

namespace Application.Services.Scoped
{
    /**
     * Service for storing a representation of event data to signify that an event has been processed
     * This design assumes events can be identified as unique by their ID (API ID never goes down always up even when items are deleted)
     */
    public interface IEventStoreService
    {

        /**
         * Take a collection of events from an input feed and remove those that have EventRecords for the given ID
         */
        public Task<List<Event>> SelectNewEvents(List<Event> events);
        /**
         * Create and persist collection of EventRecords for each event and return list of record ids
         */
        public Task<List<long>> PersistProcessedEvents(List<Event> events);
    }
}