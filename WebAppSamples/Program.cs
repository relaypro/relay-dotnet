// Copyright © 2022 Relay Inc.

using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace WebAppSamples
{
    public class Program
    {
        private static void InitLogging()
        {
            var executingDir = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
            if (null == executingDir)
            {
                return;
            }
            
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console(LogEventLevel.Verbose, 
                    outputTemplate: "{Timestamp:HH:mm:ss} [{ThreadId}] [{ThreadName}] [{Level:u3}] {Message} {NewLine}{Exception}")
                .Enrich.WithThreadId()
                .CreateLogger();
        }
        
        public static void Main(string[] args)
        {
            InitLogging();   
            
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls(new[] {@"http://127.0.0.1:3000/"});
                    webBuilder.UseStartup<Startup>();
                });
    }
}
