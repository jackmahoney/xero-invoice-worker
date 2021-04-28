
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using csharp.models;
using Newtonsoft.Json;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;

namespace csharp.services.scoped.impl
{
    public class RequestService : IRequestService
    {
        private readonly HttpClient _httpClient;

        public RequestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<EventsResponse> GetEvents(Uri url, int pageSize, int? sinceId)
        {
            var parts = new List<string>
            {
                $"pageSize={pageSize}"
            };
            if (sinceId != null)
            {
                parts.Add($"afterEventId={sinceId}");
            }
            var baseUri = new UriBuilder(url) { Query = string.Join("&", parts) };
            var json = await _httpClient.GetStringAsync(baseUri.ToString());
            return JsonConvert.DeserializeObject<EventsResponse>(json);
        }
    }
}