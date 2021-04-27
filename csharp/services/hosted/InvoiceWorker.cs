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

        public InvoiceWorker(Options options, IRunner runner)
        {
            _options = options;
            _runner = runner;
        }

        public Task StartAsync(CancellationToken cancellationToken = new())
        {
            PrintBanner();
            return Task.Factory.StartNew(async () =>
            {
                Console.WriteLine("Starting long running poll");
                while (true)
                {
                    Console.WriteLine("Running process");
                    cancellationToken.ThrowIfCancellationRequested();
                    await _runner.Process(_options.FeedUrl, _options.InvoiceDirectory);
                    Thread.Sleep(_options.RetryTimeout);
                }
            }, cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);  
        }
        

        private void PrintBanner()
        {
            
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