using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using segelServices.Services;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace segelServices
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly Posting _prosesSegel;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _prosesSegel = new Posting();
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            string path = Directory.GetCurrentDirectory();
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(path + ".txt")
                .CreateLogger();

            try
            {
                Log.Information("Services Berjalan Dengan Baik..");
                
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Services Error...");
            }
            finally
            {
                Log.CloseAndFlush();
            }

            return base.StartAsync(cancellationToken);
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _prosesSegel.Segel();
                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
