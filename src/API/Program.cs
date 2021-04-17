// ***********************************************************************
// Assembly         : APP
// Author           : jrosas
// Created          : 09-03-2020
//
// Last Modified By : jrosas
// Last Modified On : 09-03-2020
// ***********************************************************************
// <copyright file="Program.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog;
using System;
using Serilog.Formatting.Json;

namespace APP
{
    /// <summary>
    /// Class Program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
#if (DEBUG)
            Log.Logger = new LoggerConfiguration()
           .Enrich.FromLogContext()
           .WriteTo.Console()
           .WriteTo.File(new RenderedCompactJsonFormatter(), "/logs/MasterSetuplog.ndjson",
           rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 10485760, rollOnFileSizeLimit: true, retainedFileCountLimit: 3)
           .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
           .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
           .MinimumLevel.Override("System", LogEventLevel.Warning)
           .CreateLogger();
#else
       Log.Logger = new LoggerConfiguration()
         .Enrich.FromLogContext()
         .WriteTo.Console(new JsonFormatter())
         .MinimumLevel.ControlledBy(new Serilog.Core.LoggingLevelSwitch(LogEventLevel.Debug))
         .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
         .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
         .MinimumLevel.Override("System", LogEventLevel.Warning)
         .CreateLogger();
#endif

            try
            {
                Log.Information($"Starting up  {DateTime.UtcNow}!");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Error($"Something went wrong. UTC: {DateTime.UtcNow}");
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        /// <summary>
        /// Creates the host builder.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>IHostBuilder.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
#if (DEBUG)
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging.ClearProviders();
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                logging.AddDebug();
                logging.AddConsole();
                logging.AddEventSourceLogger();
                logging.AddSerilog();
            })
#endif
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel(serverOptions =>
                {
                    // Set properties and call methods on options
                })
                .UseSerilog()
                .UseIISIntegration()
                .UseStartup<Startup>();
            })
            .ConfigureAppConfiguration(appBuilder =>
            {
                appBuilder.AddEnvironmentVariables();
            });
    }
}
