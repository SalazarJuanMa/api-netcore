// ***********************************************************************
// Assembly         : Application
// Author           : jrosas
// Created          : 08-10-2020
//
// Last Modified By : jrosas
// Last Modified On : 09-04-2020
// ***********************************************************************
// <copyright file="DependencyInjection.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Core.Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Application
{
    /// <summary>
    /// Class DependencyInjection.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds the application.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddCoreApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            return services;
        }
    }
}
