// ***********************************************************************
// Assembly         :  Application.Tests
// Author           : jrosas
// Created          : 08-13-2020
//
// Last Modified By : jrosas
// Last Modified On : 08-17-2020
// ***********************************************************************
// <copyright file="Testing.cs" company=" Application.Tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Respawn;
using System.IO;
using APP;
using Microsoft.AspNetCore.Hosting;
using Moq;
using SI.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SI.Application.Common.Interfaces;
using SI.Infrastructure.services;
using System;
using NUnit.Framework;

namespace SI.Application.Tests
{
    /// <summary>
    /// Class Testing.
    /// </summary>
    [SetUpFixture]
    public class Testing
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private static IConfigurationRoot _configuration;
        /// <summary>
        /// The scope factory
        /// </summary>
        private static IServiceScopeFactory _scopeFactory;
        /// <summary>
        /// The checkpoint
        /// </summary>
        private static Checkpoint _checkpoint;

        private static IConfigurationBuilder _builder;

        private static Startup _startup;

        private static ServiceCollection _services;
        /// <summary>
        /// Runs the before any tests.
        /// </summary>
        [OneTimeSetUp]
        public static void RunBeforeAnyTests()
        {
#if (DEBUG)
            Environment.SetEnvironmentVariable("GFS_CONNSTRING", "Server=127.0.0.1;Port=5432;Database=d15rqq99p7uq09;User Id=postgres;Password=admin;");
            Environment.SetEnvironmentVariable("JWT_SECRET", "PDv7DrqznYL6nv7DrqzjnQYO9JxIsWdcjnQYL6nu0f");
            Environment.SetEnvironmentVariable("encKey", "You can count on my steel");
            Environment.SetEnvironmentVariable("EXPIRATION_IN_MINUTES", "1440");
            Environment.SetEnvironmentVariable("JWT_ISSUER", "https://localhost:44322/");
            ////Environment.SetEnvironmentVariable("ROUTE_DOCS", "C:\\MasterSetupDocs\\");
            Environment.SetEnvironmentVariable("ALLOWED_ORIGIN", "https://localhost:44322/");
            Environment.SetEnvironmentVariable("API_BASE_URL", "https://offer-setup-api-dev.app.yaengage.com");

            Environment.SetEnvironmentVariable("SMTP_SERVER", "smtp-mail.outlook.com");
            Environment.SetEnvironmentVariable("SMTP_PORT", "587");
            Environment.SetEnvironmentVariable("SMTP_USERNAME", "");
            Environment.SetEnvironmentVariable("SMTP_PASSWORD", "");
#endif

            if (_builder == null)
                _builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            if (_configuration == null)
                _configuration = _builder.Build();

            if (_services == null)
            {
                _services = new ServiceCollection();
                ISecureTokenServices example = new SecureTokenServices();
                _services.AddSingleton(provider => example);
                _services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                                        w.EnvironmentName == "Development" &&
                                        w.ApplicationName == "APP"));

                _services.AddLogging();
                _services.AddHttpClient("api", c =>
                {
                    c.DefaultRequestHeaders.Add("option", "Master Model Setup");
                });
            }

            if (_startup == null)
            {
                _startup = new Startup(_configuration);
                _startup.ConfigureServices(_services);
            }

            _scopeFactory = _services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            if (_checkpoint == null)
                _checkpoint = new Checkpoint
                {
                    TablesToIgnore = new[] { "__EFMigrationsHistory" }
                };

            EnsureDatabase();
        }

        /// <summary>
        /// Ensures the database.
        /// </summary>
        private static void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();
            GFSDataContext context = scope.ServiceProvider.GetService<GFSDataContext>();
            context.Database.Migrate();
        }

        /// <summary>
        /// send as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TResponse">The type of the t response.</typeparam>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;TResponse&gt;.</returns>
        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using IServiceScope scope = _scopeFactory.CreateScope();
            IMediator mediator = scope.ServiceProvider.GetService<IMediator>();
            return await mediator.Send(request);
        }

        /// <summary>
        /// add as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public static async Task AddAsync<TEntity>(TEntity entity)
               where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<GFSDataContext>();

            context.Add(entity);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Resets the state.
        /// </summary>
        public static async Task ResetState()
        {
            ////using var scope = _scopeFactory.CreateScope();
            ////var context = scope.ServiceProvider.GetService<GFSDataContext>();
            ////context.Database.EnsureDeleted();
            await _checkpoint.Reset(_configuration.GetConnectionString("DefaultConnection"));
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
        }
    }
}
