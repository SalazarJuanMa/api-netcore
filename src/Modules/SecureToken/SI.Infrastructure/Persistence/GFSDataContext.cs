// ***********************************************************************
// Assembly         : Infrastructure
// Author           : jrosas
// Created          : 08-10-2020
//
// Last Modified By : jrosas
// Last Modified On : 11-09-2020
// ***********************************************************************
// <copyright file="GFSDataContext.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Core.Application.Common.Constants;
using SI.Application.Common.Interfaces;
using SI.Infrastructure.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System;
using Npgsql;

namespace SI.Infrastructure.Persistence
{
    /// <summary>
    /// Class GFSDataContext.
    /// Implements the <see cref="DbContext" />
    /// Implements the <see cref="IGFSDbContext" />
    /// </summary>
    /// <seealso cref="DbContext" />
    /// <seealso cref="IGFSDbContext" />
    public partial class GFSDataContext : DbContext, IGFSDbContext
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>The connection.</value>
        private static NpgsqlConnection Connection
        {
            get
            {
                return new NpgsqlConnection(Environment.GetEnvironmentVariable(MasterModelConstants.EnviromentVarName.GFS_CONNSTRING));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GFSDataContext" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public GFSDataContext(DbContextOptions<GFSDataContext> options) : base(options)
        {
        }

        /// <summary>
        /// generic get as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">The where.</param>
        /// <returns>Task&lt;IEnumerable&lt;T&gt;&gt;.</returns>
        public async Task<IEnumerable<T>> GetAsync<T>(object where, string clause = "")
        {
            return await GenericRepository.GetAsync<T>(where, Connection, clause);
        }

        /// <summary>
        /// Called when [model creating].
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
