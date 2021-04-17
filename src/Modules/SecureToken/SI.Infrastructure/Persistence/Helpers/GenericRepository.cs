// ***********************************************************************
// Assembly         : Infrastructure
// Author           : jrosas
// Created          : 11-03-2020
//
// Last Modified By : jrosas
// Last Modified On : 11-03-2020
// ***********************************************************************
// <copyright file="GenericRepository.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Core.Application.Common.Constants;
using Core.Application.Common.Exceptions;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;
using Npgsql;

namespace SI.Infrastructure.Persistence.Helpers
{
    /// <summary>
    /// Class GenericRepository.
    /// </summary>
    public static class GenericRepository
    {
        /// <summary>
        /// generic get as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">The where.</param>
        /// <param name="connection">The connection.</param>
        /// <returns>Task&lt;IEnumerable&lt;T&gt;&gt;.</returns>
        /// <exception cref="NotFoundException"></exception>
        public static async Task<IEnumerable<T>> GetAsync<T>(object where, NpgsqlConnection connection, string clause = "")
        {
            IEnumerable<T> result;
            string tableName = typeof(T).Name;
            string query = $"SELECT {clause} * FROM {tableName} ";

            query += GenericMethods.BuildWhere(where);

            using (var conn = connection)
            {
                try
                {
                    conn.Open();
                    result = await conn.QueryAsync<T>(query);
                }
                catch (Exception exception)
                {
                    throw new NotFoundException(ExceptionConstants.Message.DATABASE_GET_EXCEPTION, exception);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
            }

            return result;
        }

    }
}
