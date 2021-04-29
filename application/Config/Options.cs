using System;
using CommandLine;

namespace Application.Config
{
    public class Options
    {
        [Option('i', "feed-url", Required = true, HelpText = "URL including protocol for JSON event feed.")]
        public Uri FeedUrl { get; set; }
        [Option('o', "invoice-dir", Required = true, HelpText = "Directory in which to save invoice PDFs.")]
        public string InvoiceDirectory { get; set; }
        [Option('r', "retry-timeout", Required = false, Default = 1500, HelpText = "Milliseconds to wait until retry endpoint in polling.")]
        public int RetryTimeout { get; set; }
        [Option('p', "page-size", Required = false, Default = 10, HelpText = "Number of items to fetch each request to feed url.")]
        public int PageSize { get; set; }
        [Option('a', "after-event-id", Required = false, Default = null, HelpText = "Event ID to start after. Useful for resuming a process.")]
        public long? AfterEventId { get; set; }
        [Option('f', "follow", Required = false, Default = false, HelpText = "To paginate through paged results and increment the after event ID.")]
        public bool Follow { get; set; }
        [Option('d', "db-path", Required = false, Default = ".events.db", HelpText = "Name of Sqlite database file to create or read from.")]
        public string DbPath { get; set; }
    }
}