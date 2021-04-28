using System.Collections.Generic;
using System.Threading.Tasks;
using csharp.models;

namespace csharp.services.scoped
{
    public interface IEventStoreService
    {

        public Task<List<Event>> SelectNewEvents(List<Event> events);
        public Task<int> PersistProcessedEvents(List<Event> events);
    }
}