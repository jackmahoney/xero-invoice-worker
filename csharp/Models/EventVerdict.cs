using System.Collections.Generic;

namespace csharp.models
{
        public record EventVerdict(List<Event> Ignore, List<Event> Persist);
}