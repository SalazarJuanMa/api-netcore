// ***********************************************************************
// Assembly         : Infrastructure
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
using Core.Application.Common.Constants;
using SI.Application.Common.Interfaces;
using SI.Infrastructure.Persistence;
using SI.Infrastructure.services;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.EntityFrameworkCore;

namespace SI.Infrastructure
{
    /// <summary>
    /// Class DependencyInjection.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds the infrastructure.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AddSignInInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<GFSDataContext>(options =>
                options.UseNpgsql(
                    Environment.GetEnvironmentVariable(MasterModelConstants.EnviromentVarName.GFS_CONNSTRING),
                    b => b.MigrationsAssembly(typeof(GFSDataContext).Assembly.FullName)));


            services.AddScoped<IGFSDbContext>(provider => provider.GetService<GFSDataContext>());
            services.AddTransient<ISecureTokenServices, SecureTokenServices>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddSingleton<ICacheServices, CacheServices>();
            return services;
        }
    }
}
