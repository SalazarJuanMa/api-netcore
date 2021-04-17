// ***********************************************************************
// Assembly         : APP.Tests
// Author           : jrosas
// Created          : 09-25-2020
//
// Last Modified By : jrosas
// Last Modified On : 11-04-2020
// ***********************************************************************
// <copyright file="Testing.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Microsoft.Extensions.DependencyInjection;

namespace APP.Tests
{
    /// <summary>
    /// Class Testing.
    /// </summary>
    public static class Testing
    {
        private static ServiceCollection _services;

        /// <summary>
        /// Runs the before any tests.
        /// </summary>
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
            Environment.SetEnvironmentVariable("SMTP_USERNAME","");
            Environment.SetEnvironmentVariable("SMTP_PASSWORD", "");

            if (_services == null)
            {
                _services = new ServiceCollection();
                _services.AddCors(options => options.AddPolicy("Cors", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(origin => true);
                }));
            }
#endif
        }
    }
}
