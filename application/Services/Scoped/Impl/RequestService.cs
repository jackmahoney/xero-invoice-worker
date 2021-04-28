using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using csharp.models;
using Microsoft.Extensions.Logging;

namespace csharp.services.scoped.impl
{
    public class RequestService : IRequestService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RequestService> _logger;

        public RequestService(HttpClient httpClient, ILogger<RequestService> logger)
        {
            _httpClient = httpClient;
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
            var response = await _httpClient.GetStringAsync(baseUri.ToString());

            // deserialize
            return JsonSerializer.Deserialize<EventsResponse>(response);
        }
    }
}