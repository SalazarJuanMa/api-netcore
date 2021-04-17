// ***********************************************************************
// Assembly         : APP
// Author           : jrosas
// Created          : 08-25-2020
//
// Last Modified By : jrosas
// Last Modified On : 04-16-2021
// ***********************************************************************
// <copyright file="AddRequiredHeaderParameter.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Core.Application.Common.Constants;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using Microsoft.OpenApi.Any;

namespace APP.Middleware
{
    /// <summary>
    /// Class AddRequiredHeaderParameter.
    /// Implements the <see cref="IOperationFilter" />
    /// </summary>
    /// <seealso cref="IOperationFilter" />
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        /// <summary>
        /// Applies the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="context">The context.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null || operation.Parameters.Count == 0) operation.Parameters = new List<OpenApiParameter>();

            ControllerActionDescriptor descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;

            string path = $"/{descriptor.RouteValues["controller"]}/V1/{descriptor.RouteValues["action"]}";

            BuildPath(operation, path);
        }

        /// <summary>
        /// Builds the path.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="path">The path.</param>
        private static void BuildPath(OpenApiOperation operation, string path)
        {
            if (!string.IsNullOrEmpty(path) &&
                !path.ToUpper().Contains(MasterModelConstants.SECURE_TOKEN) &&
                path.ToUpper().Contains(MasterModelConstants.ROUTE_CONTROLLER))
            {
                BuildSecurity(operation);

                BuildParameters(operation, "Date", "The UTC date/time at which the current rate limit window resets.");
                BuildParameters(operation, "Content-Type", "application/json");

                BuildParameters(operation, MasterModelConstants.Header.OPTION, "Selected option in main menu.");
                BuildParameters(operation, MasterModelConstants.Header.SECURE_TOKEN, "The random value.");
                BuildParameters(operation, MasterModelConstants.Header.USERID, "User Id.");

                if (path.ToUpper().Contains(MasterModelConstants.ROUTE_CONTROLLER_CHANGE))
                {
                    BuildParameters(operation, MasterModelConstants.Header.USERID, "User Id.");
                }
            }
        }


        /// <summary>
        /// Builds the security.
        /// </summary>
        /// <param name="operation">The operation.</param>
        private static void BuildSecurity(OpenApiOperation operation)
        {
            var scheme = new OpenApiSecurityScheme
            {
                Description = "JWT authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "JwtAuthorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            };

            IList<string> securityInfos = new List<string>();
            operation.Security.Add(
                new OpenApiSecurityRequirement()
                {
                    {
                        scheme , securityInfos
                    }
                }
            );
        }

        /// <summary>
        /// Generates the parameters.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        private static void BuildParameters(OpenApiOperation operation, string name, string description)
        {
            var openApiParameter = new OpenApiParameter()
            {
                Name = name,
                In = ParameterLocation.Header,
                Required = true,
                Description = string.IsNullOrEmpty(description) ? "default" : description,
                Schema = string.IsNullOrEmpty(description)
                    ? null
                    : new OpenApiSchema
                    {
                        Type = "string",
                        Description = string.IsNullOrEmpty(description) ? "default" : description,
                        Default = new Microsoft.OpenApi.Any.OpenApiString("x-random"),
                        Pattern = ""
                    }
            };

            openApiParameter.Extensions.Add("x-example", new OpenApiString("x-example"));
            openApiParameter.Schema?.Extensions.Add("x-example", new OpenApiString("x-example"));
            operation.Parameters.Add(openApiParameter);
        }
    }
}
