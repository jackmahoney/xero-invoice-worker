using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandLine;
using csharp.services.hosted;
using csharp.services.scoped;
using csharp.services.scoped.impl;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace csharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<Options>(args);
            if (result.Errors.Any())
            {
                throw new Exception("Command line parsing exception: " + result.Errors);
            }
            CreateHostBuilder(result.Value).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(Options options) =>
            Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton(options);
                    services.AddScoped<IRequestService, RequestService>();
                    services.AddScoped<IRunner, Runner>();
                    services.AddHostedService<InvoiceWorker>();
                });
    }
}
