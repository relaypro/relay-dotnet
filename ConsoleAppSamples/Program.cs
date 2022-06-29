// Copyright © 2022 Relay Inc.

using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;
using RelayDotNet;
using SamplesLibrary;
using Serilog;
using Serilog.Events;

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

            var relay = new Relay(Relay.WebSocketConnector.Fleck, "0.0.0.0", 3000, false);
            await relay.AddWorkflow("/hello_world", typeof(HelloWorldWorkflow));
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
            
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console(LogEventLevel.Verbose, 
                    outputTemplate: "{Timestamp:HH:mm:ss} [{ThreadId}] [{ThreadName}] [{Level:u3}] {Message} {NewLine}{Exception}")
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
