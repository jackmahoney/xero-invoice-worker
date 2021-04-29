using System.Reflection;
using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.db
{
    public class EventRecordContext: DbContext
    {
        
        public DbSet<EventRecord> EventRecords { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=.events.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventRecord>().ToTable("Events");
            base.OnModelCreating(modelBuilder);
        }
    }
}