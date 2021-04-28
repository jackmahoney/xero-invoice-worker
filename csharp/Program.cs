using System;
using System.Linq;
using CommandLine;
using csharp.db;
using csharp.services.hosted;
using csharp.services.scoped;
using csharp.services.scoped.impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace csharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<Options>(args);
            if (result.Errors.Any()) throw new Exception("Command line parsing exception: " + result.Errors);
            var host = CreateHostBuilder(result.Value).Build();
            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<EventRecordContext>();
                db.Database.Migrate();
            }
            host.Run();
        }

        private static IHostBuilder CreateHostBuilder(Options options)
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    // add db, cli options, and http client
                    services.AddDbContext<EventRecordContext>();
                    services.AddHttpClient("http").SetHandlerLifetime(TimeSpan.FromMilliseconds(options.RetryTimeout));
                    services.AddSingleton(options);
                    // add scoped services
                    services.AddScoped<IEventStoreService, EventStoreService>();
                    services.AddScoped<IFileService, FileService>();
                    services.AddScoped<IInvoiceService, InvoiceService>();
                    services.AddScoped<IPdfService, PdfService>();
                    services.AddScoped<IRequestService, RequestService>();
                    services.AddScoped<IRunner, Runner>();
                    // add the hosted service that will run process
                    services.AddHostedService<InvoiceWorker>();
                });
        }
    }
}