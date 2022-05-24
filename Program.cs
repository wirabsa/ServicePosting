using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySqlConnector;
using segelServices.Settings;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace segelServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //string filename = @"C:\CMA\logServices.txt";
            //var lines = File.ReadLines(filename);
            AppSettings.ConnString = new MySqlConnectionStringBuilder()
            {
                Server = "103.135.24.11",
                Port = 3309,
                UserID = "bimasakti",
                Password = "bimasaktisanjaya2017",
                Database = "bshpd_develop",
                ConnectionTimeout = 60

                /*
                Server = "10.30.123.202",
                Port = 3306,
                UserID = "bimasakti",
                Password = "bimasaktisanjaya2017",
                Database = "bshpd_develop"
                */
            }.ConnectionString;

            CreateHostBuilder(args).Build().Run();


        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                })
            .UseSerilog();
    }
}
