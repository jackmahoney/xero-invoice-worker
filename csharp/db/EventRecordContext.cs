using System.Reflection;
using csharp.models;
using Microsoft.EntityFrameworkCore;

namespace csharp.db
{
    public class EventRecordContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=events.db", options =>
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