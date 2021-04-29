using System;
using System.Linq;
using Application.Config;
using Application.db;
using Application.Services.Hosted;
using Application.Services.Scoped;
using Application.Services.Scoped.Impl;
using CommandLine;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Application
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
                    services.AddScoped<IHttpService, HttpService>();
                    services.AddScoped<IInvoiceService, InvoiceService>();
                    services.AddScoped<IPdfService, PdfService>();
                    services.AddScoped<ITemplatingService, TemplatingService>();
                    services.AddScoped<IRequestService, RequestService>();
                    services.AddScoped<IRunner, Runner>();
                    // add the hosted service that will run process
                    services.AddHostedService<InvoiceWorker>();

                    // if developing for production hosted environment would enable metrics server here for health checks and prometheus etc
                    /*
                    services.AddHealthChecks();
                    services.AddPrometheus();
                    */
                });
        }
    }
}