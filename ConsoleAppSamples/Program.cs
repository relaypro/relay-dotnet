// Copyright © 2022 Relay Inc.

using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RelayDotNet;
using SamplesLibrary;
using Serilog;
using Serilog.Events;
using Serilog.Configuration;

namespace ConsoleAppSamples
{
    internal static class Program
    {
        private static readonly ManualResetEvent TerminationEvent = new ManualResetEvent(false);

        public static async Task Main()
        {
            InitLogging();
            
            AppDomain.CurrentDomain.ProcessExit += CurrentDomainOnProcessExit;
            AssemblyLoadContext.Default.Unloading += DefaultOnUnloading;
            Console.CancelKeyPress += ConsoleOnCancelKeyPress;
            
            Log.Debug("***********************************************");
            Log.Debug("*     RelayDotNet Console Sample Starting     *");
            Log.Debug("***********************************************");

            var relay = new Relay(Relay.WebSocketConnector.Fleck, "0.0.0.0", 8080, false);
            await relay.AddWorkflow("/hellopath", typeof(HelloWorldWorkflow));
            relay.Start();

            TerminationEvent.WaitOne();
            
            relay.Dispose();
            
            Log.Debug("***********************************************");
            Log.Debug("*     RelayDotNet Console Sample Stopping     *");
            Log.Debug("***********************************************");
            
            Log.CloseAndFlush();
        }
        
        private static void InitLogging()
        {
            var executingDir = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
            if (null == executingDir)
            {
                return;
            }

        // read logging config from appsettings.json instead of hardcoding MinimumLevel
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
            .Build();
            
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console(LogEventLevel.Verbose, 
                    outputTemplate: "{Timestamp:HH:mm:ss.fff} [{ThreadId}] [{ThreadName}] [{Level:u3}] {Message} {NewLine}{Exception}")
                .Enrich.WithThreadId()
                .CreateLogger();
        }

        private static void CurrentDomainOnProcessExit(object sender, EventArgs eventArgs)
        {
            TerminationEvent.Set();
        }

        private static void DefaultOnUnloading(AssemblyLoadContext assemblyLoadContext)
        {
            TerminationEvent.Set();
        }

        private static void ConsoleOnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            TerminationEvent.Set();
        }
    }
}
