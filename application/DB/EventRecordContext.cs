using System.Reflection;
using Application.Config;
using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.db
{
    public class EventRecordContext : DbContext
    {
        private readonly Options _options;
        public EventRecordContext(Options options)
        {
            _options = options;
        }

        public DbSet<EventRecord> EventRecords { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_options.DbPath}", options =>
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