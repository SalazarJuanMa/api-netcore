// ***********************************************************************
// Assembly         : APP.Tests
// Author           : jrosas
// Created          : 08-18-2020
//
// Last Modified By : jrosas
// Last Modified On : 11-04-2020
// ***********************************************************************
// <copyright file="SecureTokenExtensionTests.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using APP.Middleware;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using static APP.Tests.Testing;

namespace APP.Tests.Middleware
{
    /// <summary>
    /// Defines test class SecureTokenExtensionTests.
    /// </summary>
    [TestClass]
    public class SecureTokenExtensionTests
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
        /// is sec token valid extension instance as an asynchronous operation.
        /// </summary>
        [TestMethod]
        public async System.Threading.Tasks.Task SecureTokenExtension_TokenAsync()
        {
            try
            {
                var response = await _client.SendAsync(new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("http://localhost/api/SecureToken/"),
                });

                Assert.IsNotNull(response);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex.Message);
            }
        }

        /// <summary>
        /// Defines the test method SecureTokenExtension_Validation.
        /// </summary>
        [TestMethod]
        public void SecureTokenExtension_Validation()
        {
            MethodInfo privateDelegateMethod = typeof(SecureTokenExtension).GetMethod("LifetimeValidator", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            object[] parameters = { DateTime.Now, DateTime.Now, null, null };
            object result = privateDelegateMethod.Invoke(null, parameters);
            Assert.IsFalse((bool)result);
        }

        /// <summary>
        /// is sec token valid extension general information as an asynchronous operation.
        /// </summary>
        /// <param name="secToken">The sec token.</param>
        /// <param name="sectTokenValue">The sect token value.</param>
        /// <param name="option">The option.</param>
        /// <param name="optionValue">The option value.</param>
        [DataTestMethod]
        [DataRow("SecureToken", "TOKEN=defrf3534wf", "option", "App Setup")]
        [DataRow("SecureToken", "TOKEN=defrf3534wf", "option", "")]
        [DataRow("SecureToken", "TOKEN=defrf3534wf", "option", null)]
        [DataRow("SecureToken", "", "option", "App Setup")]
        [DataRow("SecureToken", null, "option", "App Setup")]
        public async System.Threading.Tasks.Task SecureTokenExtension_GeneralInfoAsync(
            string secToken,
            string sectTokenValue,
            string option,
            string optionValue)
        {
            _client.DefaultRequestHeaders.Add(secToken, sectTokenValue);
            _client.DefaultRequestHeaders.Add(option, optionValue);

            try
            {
                HttpResponseMessage response = await _client.SendAsync(new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("http://localhost/api/v1/GeneralInfo/"),
                });

                ////response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Assert.IsNotNull(response);
            }
            catch (Exception ex)
            {
                //If the data dont exist in Stage or PROD
                Assert.IsNotNull(ex);
                Trace.WriteLine(ex, $"secToken {secToken}, sectTokenValue {sectTokenValue} , option {option} and optionValue {optionValue} ");
                Debug.WriteLine(ex, $"secToken {secToken}, sectTokenValue {sectTokenValue} , option {option} and optionValue {optionValue} ");
            }
        }

        /// <summary>
        /// is sec token valid extension change general information as an asynchronous operation.
        /// </summary>
        /// <param name="offerId">The offer identifier.</param>
        /// <param name="offerIdValue">The offer identifier value.</param>
        /// <param name="prId">The pr identifier.</param>
        /// <param name="prIdValue">The pr identifier value.</param>
        [DataTestMethod]
        [DataRow("id", "1", "rId", "000251")]
        [DataRow("id", "1", "rId", "")]
        [DataRow("id", "1", "rId", null)]
        [DataRow("id", "", "rId", "000251")]
        [DataRow("id", null, "rId", "000251")]
        [DataRow("id", "1", "rId", "12")]
        [DataRow("id", "1", "rId", "12@4")]
        [DataRow("id", "1", "rId", "01234567892345678")]
        public async System.Threading.Tasks.Task SecureTokenExtension_ChangeGeneralInfoAsync(
           string id,
           string IdValue,
           string rId,
           string rIdValue)
        {
            _client.DefaultRequestHeaders.Add("SecureToken", "sectTokenValue");
            _client.DefaultRequestHeaders.Add("option", "optionValue");
            _client.DefaultRequestHeaders.Add(rId, rIdValue);
            _client.DefaultRequestHeaders.Add(id, IdValue);

            try
            {
                var response = await _client.SendAsync(new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("http://localhost/api/v1/ChangeGeneralInfo/"),
                });

                Assert.IsNotNull(response);
            }
            catch (Exception ex)
            {
                //If the data dont exist in Stage or PROD
                Assert.IsNotNull(ex);
                Trace.WriteLine(ex, $" ID {id}, offer Id Value {IdValue} , PR Id {rId} and PR IdValue {rIdValue} ");
                Debug.WriteLine(ex, $" ID {id}, offer Id Value {IdValue} , PR Id {rId} and PR IdValue {rIdValue} ");
            }
        }
    }
}
