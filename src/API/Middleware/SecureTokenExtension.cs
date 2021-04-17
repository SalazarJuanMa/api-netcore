// ***********************************************************************
// Assembly         : APP
// Author           : jrosas
// Created          : 08-17-2020
//
// Last Modified By : jrosas
// Last Modified On : 04-16-2021
// ***********************************************************************
// <copyright file="SecureTokenExtension.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Core.Application.Common.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System;

namespace APP.Middleware
{
    /// <summary>
    /// Class SecureTokenExtension.
    /// </summary>
    public static class SecureTokenExtension
    {
        /// <summary>
        /// Gets the JWT issuer.
        /// </summary>
        /// <value>The JWT issuer.</value>
        private static string JwtIssuer
        {
            get
            {
                return Environment.GetEnvironmentVariable(MasterModelConstants.EnviromentVarName.JWT_ISSUER);
            }
        }

        /// <summary>
        /// Gets the JWT secret.
        /// </summary>
        /// <value>The JWT secret.</value>
        private static string JwtSecret
        {
            get
            {
                return Environment.GetEnvironmentVariable(MasterModelConstants.EnviromentVarName.JWT_SECRET);
            }
        }

        /// <summary>
        /// Adds the is sec token valid.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddSecureToken(this IServiceCollection services)
        {
            string secret = JwtSecret;
            string issuer = JwtIssuer;
            string audience = MasterModelConstants.JWT_AUDIENCE;

            byte[] key = Encoding.ASCII.GetBytes(secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidIssuer = issuer,
                    ValidateAudience = false,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    LifetimeValidator = LifetimeValidator,
                };

                x.Events = GenerateCodeJwtBarer();
            });

            return services;
        }

        /// <summary>
        /// Generates the code JWT barer.
        /// </summary>
        /// <returns>JwtBearerEvents.</returns>
        private static JwtBearerEvents GenerateCodeJwtBarer()
        {
            return new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var path = context.HttpContext.Request.Path;

                    if (path.ToString().ToUpper().Contains(MasterModelConstants.ROUTE_CONTROLLER))
                    {
                        ValidationForHeader(
                            context,
                            context.Request.Headers[MasterModelConstants.Header.SECURE_TOKEN],
                            context.Request.Headers[MasterModelConstants.Header.OPTION],
                            context.Request.Headers[MasterModelConstants.Header.USERID]);

                        if (path.ToString().ToUpper().Contains(MasterModelConstants.ROUTE_CONTROLLER_CHANGE))
                        {
                            ValidationsForHeaderInChange(
                                context,
                                context.Request.Headers[MasterModelConstants.Header.OPTION],
                                context.Request.Headers[MasterModelConstants.Header.USERID]);
                        }

                        context.Token = context.Request.Headers[MasterModelConstants.Header.SECURE_TOKEN];
                    }

                    return Task.CompletedTask;
                }
            };
        }

        /// <summary>
        /// Validations for header.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="option">The option.</param>
        /// <param name="userId">The user identifier.</param>
        private static void ValidationForHeader(
            MessageReceivedContext context,
            Microsoft.Extensions.Primitives.StringValues accessToken,
            Microsoft.Extensions.Primitives.StringValues option,
            Microsoft.Extensions.Primitives.StringValues userId)
        {
            if (accessToken.Count == 0 || string.IsNullOrEmpty(accessToken) ||
                         option.Count == 0 || string.IsNullOrEmpty(option) ||
                         userId.Count == 0 || string.IsNullOrEmpty(userId))
            {
                GenerateCodeException(context, MasterModelConstants.Header.TOKEN_OR_OPTION_REQUIRED);
            }
        }

        /// <summary>
        /// Validations for header in change.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="option">The option.</param>
        /// <param name="userId">The user identifier.</param>

        private static void ValidationsForHeaderInChange(
    MessageReceivedContext context,
    Microsoft.Extensions.Primitives.StringValues option,
    Microsoft.Extensions.Primitives.StringValues userId)
        {
        }

        /// <summary>
        /// Generates the code exception.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="msg">The MSG.</param>
        /// <returns>Task.</returns>
        private static Task GenerateCodeException(MessageReceivedContext context, string msg)
        {
            string result = JsonConvert.SerializeObject(new
            {
                status = StatusCodes.Status400BadRequest,
                title = "An error occurred while processing your request.",
                detail = msg
            });
            context.Response.StatusCode = 400;
            context.Response.ContentType = MasterModelConstants.CONTENT_TYPE_JSON;
            return context.Response.WriteAsync(result);
        }

        /// <summary>
        /// Lifetimes the validator.
        /// </summary>
        /// <param name="notBefore">The not before.</param>
        /// <param name="expires">The expires.</param>
        /// <param name="securityToken">The token.</param>
        /// <param name="validationParameters">The parameters.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            return expires != null && expires > DateTime.UtcNow;
        }
    }
}
