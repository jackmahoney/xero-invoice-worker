using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace csharp.models
{
    
    [Index(nameof(Hash))]
    public class EventRecord
    {
        [Key]
        public int Id { get; set; }
        public string Hash { get; set; }
        public DateTime CreatedAt{ get; set; }
    }
}