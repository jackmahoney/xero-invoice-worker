
using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using csharp.models;
using Newtonsoft.Json;

namespace csharp.services.scoped.impl
{
    public class RequestService : IRequestService
    {
        public async Task<EventsResponse> GetEvents(Uri url)
        {
            
            var client = new HttpClient(){ Timeout = TimeSpan.FromMilliseconds(3000)};
            var json = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<EventsResponse>(json);
        }
    }
}