using System;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Models;

namespace Application.Services.Scoped
{
    public interface IRequestService
    {

        public Task<EventsResponse> GetEvents(Uri url, int pageSize, long? sinceId);
    }
}