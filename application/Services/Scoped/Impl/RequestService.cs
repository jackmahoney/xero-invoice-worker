using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Models;
using Application.Services.Scoped;
using Microsoft.Extensions.Logging;

namespace Application.Services.Scoped.Impl
{
    public class RequestService : IRequestService
    {
        private readonly IHttpService _httpService;
        private readonly ILogger<RequestService> _logger;

        public RequestService(IHttpService httpService, ILogger<RequestService> logger)
        {
            _httpService = httpService;
            _logger = logger;
        }
        
        public async Task<EventsResponse> GetEvents(Uri url, int pageSize, long? sinceId)
        {
            // create request parameters 
            var parts = new List<string> {$"pageSize={pageSize}"};
            if (sinceId != null) parts.Add($"afterEventId={sinceId}");
            // create url with params
            var baseUri = new UriBuilder(url) {Query = string.Join("&", parts)};

            // call url
            _logger.LogInformation($"Calling {baseUri}");
            var response = await _httpService.GetStringAsync(baseUri.ToString());

            // deserialize
            return JsonSerializer.Deserialize<EventsResponse>(response, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
        }
    }
}