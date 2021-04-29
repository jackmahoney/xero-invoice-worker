using System.Collections.Generic;

namespace Application.Models
{
    public record EventVerdict(List<Event> Ignore, List<Event> Persist);
}