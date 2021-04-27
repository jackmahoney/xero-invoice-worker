using System;
using System.Collections.Generic;
using System.IO;
using CommandLine;

namespace csharp
{
    class Program
    {
        private class Options
        {
            [Option('i', "feed-url", Required = true, HelpText = "URL including protocol for JSON event feed.")]
            public Uri FeedUrl { get; set; }
            [Option('o', "invoice-dir", Required = true, HelpText = "Directory in which to save invoice PDFs.")]
            public string InvoiceDirectory { get; set; }
        }
        static void Main(string[] args)
        {
            
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunOptions)
                .WithNotParsed(HandleParseError);
        }
      

    }
}
