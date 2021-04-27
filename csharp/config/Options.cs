using System;
using CommandLine;

namespace csharp
{
    public class Options
         {
             [Option('i', "feed-url", Required = true, HelpText = "URL including protocol for JSON event feed.")]
             public Uri FeedUrl { get; set; }
             [Option('o', "invoice-dir", Required = true, HelpText = "Directory in which to save invoice PDFs.")]
             public string InvoiceDirectory { get; set; }
         }
}