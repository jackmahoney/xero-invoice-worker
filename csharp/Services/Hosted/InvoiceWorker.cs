using System;
using System.Threading;
using System.Threading.Tasks;
using csharp.services.scoped;
using Microsoft.Extensions.Hosting;

namespace csharp.services.hosted
{
    public class InvoiceWorker : IHostedService
    {
        private readonly Options _options;
        private readonly IRunner _runner;
        private readonly IFileService _fileService;

        public InvoiceWorker(Options options, IRunner runner, IFileService fileService)
        {
            _options = options;
            _runner = runner;
            _fileService = fileService;
        }

        public Task StartAsync(CancellationToken cancellationToken = new())
        {
            // setup 
            PrintBanner();
            _fileService.CreateDirectoryIfNotExists(_options.InvoiceDirectory);

            // start long running task to process invoice event feed
            return Task.Factory.StartNew(async () =>
            {
                Console.WriteLine("Starting long running poll");
                while (true)
                {
                    Console.WriteLine("Running process");
                    cancellationToken.ThrowIfCancellationRequested();
                    // run invoice worker process
                    await _runner.Process(_options.FeedUrl, _options.InvoiceDirectory);
                    // wait before polling again
                    Thread.Sleep(_options.RetryTimeout);
                }
            }, cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);  
        }
        

        private void PrintBanner()
        {
            // log config at startup 
            var banner = new []
            {
                "-------------------------",
                "Starting InvoiceWorker:",
                $"feed = {_options.FeedUrl}",
                $"output = {_options.InvoiceDirectory}",
                "-------------------------\n",
            };
            Console.Write(string.Join(Environment.NewLine, banner));
        }

        public Task StopAsync(CancellationToken cancellationToken = new())
        {
            throw new NotImplementedException();
        }
    }
}