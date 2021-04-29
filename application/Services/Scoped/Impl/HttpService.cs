using System.Net.Http;
using System.Threading.Tasks;
using Application.Services.Scoped;

namespace Application.Services.Scoped.Impl
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetStringAsync(string url)
        {
            return await _httpClient.GetStringAsync(url);
        }
    }
}