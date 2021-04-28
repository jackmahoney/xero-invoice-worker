using System;
using System.Net.Http;
using System.Threading.Tasks;
using csharp.models;

namespace csharp.services.scoped
{
    public interface IRequestService
    {

        public Task<EventsResponse> GetEvents(Uri url, int pageSize, long? sinceId);
    }
}