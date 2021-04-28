using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace csharp.services.scoped
{
    public interface IRunner
    {
        /**
         * Process events from an event feed
         * Compare response with known processed events, filter out already processed,
         * process new events and save or delete a PDF file depending on the event type
         * Returns ID of last processed event (assumes event feed is ordered by ascending ID)
         */
        public Task<long?> Process(Uri inputUrl, string outputDir, int pageSize, long? lastId);
    }
}