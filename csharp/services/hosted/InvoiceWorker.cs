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
        private readonly IRequestService _requestService;

        public InvoiceWorker(Options options, IRequestService requestService)
        {
            _options = options;
            _requestService = requestService;
        }

        public Task StartAsync(CancellationToken cancellationToken = new CancellationToken())
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
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}