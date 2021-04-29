using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Config;
using Application.Services.Scoped;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Application.Services.Hosted
{
    public class InvoiceWorker : IHostedService
    {
        private readonly Options _options;
        private readonly IRunner _runner;
        private readonly IFileService _fileService;
        private readonly ILogger<InvoiceWorker> _logger;

        public InvoiceWorker(Options options, IRunner runner, IFileService fileService, ILogger<InvoiceWorker> logger)
        {
            _options = options;
            _runner = runner;
            _fileService = fileService;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken = new())
        {
            // setup 
            PrintBanner();
            _fileService.CreateDirectoryIfNotExists(_options.InvoiceDirectory);

            // start long running task to process invoice event feed
            return Task.Factory.StartNew(async () =>
            {
                // use an optional passed event id to start at or start from null
                var lastId = _options.AfterEventId;
                _logger.LogInformation("Starting long running poll");
                while (true)
                {
                    _logger.LogInformation("Running process");
                    cancellationToken.ThrowIfCancellationRequested();
                    // run invoice worker process 
                    var newId = await _runner.Process(_options.FeedUrl, _options.InvoiceDirectory, _options.PageSize, lastId);
                    // set the lastId to the ID returned if follow enabled
                    if (_options.Follow)
                    {
                        lastId = newId;
                    }
                    // wait before polling again
                    Thread.Sleep(_options.RetryTimeout);
                }
            }, cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }


        private void PrintBanner()
        {
            // log config at startup 
            var banner = new[]
            {
                "-------------------------",
                "Starting InvoiceWorker:",
                $"feed = {_options.FeedUrl}",
                $"output = {_options.InvoiceDirectory}",
                "-------------------------\n",
            };
            _logger.LogInformation(string.Join(Environment.NewLine, banner));
        }

        public Task StopAsync(CancellationToken cancellationToken = new())
        {
            return Task.Run(() => _logger.LogInformation("Cancelling invoice worker"), cancellationToken);
        }
    }
}