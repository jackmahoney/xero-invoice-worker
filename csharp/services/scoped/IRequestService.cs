using System;
using System.Net.Http;
using System.Threading.Tasks;
using csharp.events;

namespace csharp.services.scoped
{
    public interface IRequestService
    {

        public Task<Events> GetEvents(Uri url);
    }
}