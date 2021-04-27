using System;
using System.Threading.Tasks;

namespace csharp.services.scoped.impl
{
    public class Runner : IRunner
    {
        private readonly IRequestService _requestService;

        public Runner(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task Process(Uri inputUrl, string outputDir)
        {
            Console.WriteLine($"Polling {inputUrl}");
            var events = await _requestService.GetEvents(inputUrl);
            Console.WriteLine(events);
        }
    }
}