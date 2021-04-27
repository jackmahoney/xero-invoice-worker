
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using csharp.events;

namespace csharp.services.scoped.impl
{
    public class RequestService : IRequestService
    {
        public async Task<Events> GetEvents(Uri url)
        {
            
            var client = new HttpClient(){ Timeout = TimeSpan.FromMilliseconds(3000)};
            var streamTask = client.GetStreamAsync(url);
            return await JsonSerializer.DeserializeAsync<Events>(await streamTask);
        }
    }
}