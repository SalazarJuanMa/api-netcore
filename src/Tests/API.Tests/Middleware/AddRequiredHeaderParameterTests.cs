// ***********************************************************************
// Assembly         : APP.Tests
// Author           : jrosas
// Created          : 08-25-2020
//
// Last Modified By : jrosas
// Last Modified On : 02-16-2021
// ***********************************************************************
// <copyright file="AddRequiredHeaderParameterTests.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static APP.Tests.Testing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Serilog;
using APP.Middleware;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System.Collections.Generic;
using System;
using Moq;

namespace APP.Tests.Middleware
{
    /// <summary>
    /// Defines test class AddRequiredHeaderParameterTests.
    /// </summary>
    [TestClass]
    public class AddRequiredHeaderParameterTests
    {
        /// <summary>
        /// The server
        /// </summary>
        private TestServer _server;
        /// <summary>
        /// The client
        /// </summary>
        private HttpClient _client;

        /// <summary>
        /// Tests the initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            RunBeforeAnyTests();
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", true, true)
             .AddEnvironmentVariables();
            var configuration = builder.Build();

            _server = new TestServer(new WebHostBuilder()
             .ConfigureServices(svc =>
             {
                 svc.AddSingleton(configuration); // attempt 2
             })
             .UseConfiguration(configuration).UseSerilog()
                       .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            _server = null;
            _client = null;
        }

        /// <summary>
        /// AddRequiredHeaderParameter_TokenAsync instance as an asynchronous operation.
        /// </summary>
        [TestMethod]
        public async Task AddRequiredHeaderParameter_TokenAsync()
        {
            var response = await _client.GetAsync("/swagger/v1/swagger.json");
            Assert.IsNotNull(response);
        }

        /// <summary>
        /// Defines the test method AddRequiredHeaderParameter_Private_Method.
        /// </summary>
        [TestMethod]
        public void AddRequiredHeaderParameter_Private_Method()
        {
            var a = new AddRequiredHeaderParameter();
            MethodInfo methodInfo = typeof(AddRequiredHeaderParameter).GetMethod("BuildParameters",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            object[] parameters = { new OpenApiOperation(), "name", "offer" };

            methodInfo.Invoke(a, parameters);
        }

        /// <summary>
        /// Defines the test method AddRequiredHeaderParameter_Private_Method_empty.
        /// </summary>
        [TestMethod]
        public void AddRequiredHeaderParameter_Private_Method_empty()
        {
            var a = new AddRequiredHeaderParameter();
            MethodInfo methodInfo = typeof(AddRequiredHeaderParameter).GetMethod("BuildParameters",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            object[] parameters = { new OpenApiOperation(), string.Empty, string.Empty };

            methodInfo.Invoke(a, parameters);
        }

        /// <summary>
        /// Defines the test method AddRequiredHeaderParameter_BuildSecurity_Private_Method.
        /// </summary>
        [TestMethod]
        public void AddRequiredHeaderParameter_BuildSecurity_Private_Method()
        {
            var a = new AddRequiredHeaderParameter();
            MethodInfo methodInfo = typeof(AddRequiredHeaderParameter).GetMethod("BuildSecurity",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            object[] parameters = { new OpenApiOperation() };

            methodInfo.Invoke(a, parameters);
        }

        /// <summary>
        /// Defines the test method AddRequiredHeaderParameter_BuildPath_Private_Method.
        /// </summary>
        [TestMethod]
        public void AddRequiredHeaderParameter_BuildPath_Private_Method()
        {
            var a = new AddRequiredHeaderParameter();
            MethodInfo methodInfo = typeof(AddRequiredHeaderParameter).GetMethod("BuildPath",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            object[] parameters = { new OpenApiOperation(), "/API/V1/GeneralInfo" };

            methodInfo.Invoke(a, parameters);
        }

        /// <summary>
        /// Defines the test method AddRequiredHeaderParameter_BuildPath_Private_Method_Change.
        /// </summary>
        [TestMethod]
        public void AddRequiredHeaderParameter_BuildPath_Private_Method_Change()
        {
            var a = new AddRequiredHeaderParameter();
            MethodInfo methodInfo = typeof(AddRequiredHeaderParameter).GetMethod("BuildPath",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            object[] parameters = { new OpenApiOperation(), "/API/V1/CHANGEGeneralInfo" };

            methodInfo.Invoke(a, parameters);
        }

        /// <summary>
        /// Defines the test method AddRequiredHeaderParameter_Apply_Method_Exception.
        /// </summary>
        [TestMethod]
        public void AddRequiredHeaderParameter_Apply_Method_Exception()
        {
            try
            {
                AddRequiredHeaderParameter addRequiredHeaderParameter = new AddRequiredHeaderParameter();
                OpenApiOperation operation = new OpenApiOperation();
                addRequiredHeaderParameter.Apply(operation, null);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
                Assert.IsNotNull(ex.Message);
            }
        }

        /// <summary>
        /// Defines the test method AddRequiredHeaderParameter_Apply_Method_Exception_Info.
        /// </summary>
        [TestMethod]
        public void AddRequiredHeaderParameter_Apply_Method_Exception_Info()
        {
            try
            {
                AddRequiredHeaderParameter addRequiredHeaderParameter = new AddRequiredHeaderParameter();
                OpenApiOperation operation = new OpenApiOperation()
                {
                    Parameters = new List<OpenApiParameter>()
                };

                Mock<MethodInfo> methodInfo = new Mock<MethodInfo>();
                OperationFilterContext filterContext = new OperationFilterContext(
                    apiDescription: new ApiDescription()
                    {
                        ActionDescriptor = new ActionDescriptor()
                        {
                            RouteValues = new Dictionary<string, string>()
                            {
                            { "controller", "v1/A" }
                            }
                        }
                    },
                    schemaRegistry: null,
                    schemaRepository: null,
                    methodInfo: methodInfo.Object);

                addRequiredHeaderParameter.Apply(operation, filterContext);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
                Assert.IsNotNull(ex.Message);
            }
        }
    }
}
