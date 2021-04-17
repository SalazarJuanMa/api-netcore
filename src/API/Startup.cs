// ***********************************************************************
// Assembly         : APP
// Author           : jrosas
// Created          : 09-03-2020
//
// Last Modified By : jrosas
// Last Modified On : 11-09-2020
// ***********************************************************************
// <copyright file="Startup.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using APP.Constants;
using APP.Filters;
using APP.Middleware;
using Core.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;
using SI.Application;
using SI.Infrastructure;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SI.Infrastructure.Persistence;
using Swashbuckle.AspNetCore.Filters;

namespace APP
{
    /// <summary>
    /// Class Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Gets the API base URL.
        /// </summary>
        /// <value>The API base URL.</value>
        private static string APIBaseURL
        {
            get
            {
                return Environment.GetEnvironmentVariable(StartupConstants.API_BASE_URL);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            ConfigureModules(services);


            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddDbContextCheck<GFSDataContext>();

            services.AddControllersWithViews(options =>
                          options.Filters.Add(new ApiExceptionFilter())).AddNewtonsoftJson();

            services.AddMvc()
          .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
          .AddNewtonsoftJson();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
                options.SuppressMapClientErrors = true;
            });


            // Register the Swagger generator, defining 1 or more Swagger documents
            ConfigureSwagger(services);

            ////services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();

            services.AddHttpClient(StartupConstants.API, client =>
            {
                var ApiURI = APIBaseURL;
                client.BaseAddress = new Uri(ApiURI);
                client.DefaultRequestHeaders.Add(StartupConstants.ACCEPT, StartupConstants.CONTENT_TYPE_JSON);
            });
        }

        private static void ConfigureModules(IServiceCollection services)
        {
            services.AddCoreApplication();
            services.AddSecureToken();
            ////SignIn
            services.AddSignInApplication();
            services.AddSignInInfrastructure();
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<AddRequiredHeaderParameter>();
                c.OperationFilter<AddResponseHeadersFilter>(); // [SwaggerResponseHeader]

                c.SwaggerDoc(StartupConstants.Swagger.VERSION, new OpenApiInfo
                {
                    Version = StartupConstants.Swagger.VERSION,
                    Title = StartupConstants.Swagger.TITLE,
                    Description = StartupConstants.Swagger.DESCRIPTION,
                    TermsOfService = new Uri(StartupConstants.Swagger.TERM_SERVICE_URL),
                    Contact = new OpenApiContact
                    {
                        Name = StartupConstants.Swagger.SUPPORT,
                        Email = StartupConstants.Swagger.EMAIL,
                        Url = new Uri(StartupConstants.Swagger.CONTACT_URL),
                    },
                    License = new OpenApiLicense
                    {
                        Name = StartupConstants.Swagger.POLICY,
                        Url = new Uri(StartupConstants.Swagger.LICENSE_URL)
                    }
                });


                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.DescribeAllParametersInCamelCase();
                c.CustomSchemaIds((type) => type.FullName);
                c.EnableAnnotations(true, true);
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();               
            }
            else
            {
                app.UseHsts();
            }

            HealthCheckOptions options = new HealthCheckOptions
            {
                ResponseWriter = async (c, r) =>
                {
                    c.Response.ContentType = StartupConstants.CONTENT_TYPE_JSON;

                    string result = JsonConvert.SerializeObject(new
                    {
                        status = r.Status.ToString(),
                        message = r.Entries.Select(e => new { key = e.Key, value = e.Value.Status.ToString() })
                    });
                    await c.Response.WriteAsync(result);
                }
            };

// Enable middleware to serve generated Swagger as a JSON endpoint.
                var basePath = $"/";
                app.UseSwagger(c =>
                {
                    c.SerializeAsV2 = true;
                    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                    {
                        swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{basePath}" } };
                    });
                });

                app.UseSwaggerUI(c =>
                {
                    c.RoutePrefix = string.Empty;
                    c.SwaggerEndpoint(StartupConstants.Swagger.URL_JSON, StartupConstants.Swagger.NAME);
                    c.SupportedSubmitMethods(Array.Empty<SubmitMethod>());
                    c.DocExpansion(DocExpansion.None);
                    c.EnableDeepLinking();
                    c.ShowExtensions();
                    c.ShowCommonExtensions();
                    c.EnableValidator();
                });

            app.UseHealthChecks(StartupConstants.HEALTH_CHECK, options);
            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseSerilogRequestLogging();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: StartupConstants.DEFAULT,
                    pattern: StartupConstants.CONTROLLER_ROUTE);
            });
        }
    }
}
