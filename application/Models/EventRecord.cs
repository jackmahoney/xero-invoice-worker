using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Application.Models
{
    
    [Index(nameof(Hash), nameof(EventId))]
    public class EventRecord
    {
        [Key]
        public long Id { get; set; }
        public long EventId { get; set; }
        public string Hash { get; set; }
        public DateTime CreatedAt{ get; set; }
    }
}