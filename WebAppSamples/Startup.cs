// Copyright © 2022 Relay Inc.

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RelayDotNet;
using SamplesLibrary;
using Serilog;

namespace WebAppSamples
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            Relay relay = new Relay(Relay.WebSocketConnector.SystemKestral, "0.0.0.0", 3000, false);
            relay.AddWorkflow("/hello_world", typeof(HelloWorldWorkflow));
            relay.Start();
            services.AddSingleton(typeof(Relay), relay);
            Log.Debug("added relay service");
            services.AddTransient<WebSocketMiddleware>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseWebSockets(new WebSocketOptions
            {
                KeepAliveInterval = TimeSpan.FromSeconds(60)
            });
            app.UseMiddleware<WebSocketMiddleware>();
        }
    }
}
