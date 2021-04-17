// ***********************************************************************
// Assembly         : Infrastructure
// Author           : jrosas
// Created          : 08-10-2020
//
// Last Modified By : jrosas
// Last Modified On : 11-03-2020
// ***********************************************************************
// <copyright file="GenericMethods.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Core.Application.Common.Exceptions;
using System.Linq;
using System.Reflection;

namespace SI.Infrastructure.Persistence.Helpers
{
    /// <summary>
    /// Class GenericMethods.
    /// </summary>
    public static class GenericMethods
    {
        /// <summary>
        /// Builds the where.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="NotFoundException">The Clause WHERE it must have filtering parameters.</exception>
        public static string BuildWhere(object where)
        {
            string query = string.Empty;
            if (where == null || IsAnyNullOrEmpty(where))
            {
                throw new NotFoundException("The WHERE clause should must have the filtering parameters.");
            }
            else if (where != null)
            {
                int i = 0;
                foreach (PropertyInfo c in where.GetType().GetProperties())
                {
                        switch (i)
                        {
                            case 0:
                                query += GetValuesFromWhere(c.Name, c.GetValue(where, null), "WHERE");
                                i++;
                                break;
                            default:
                                query += GetValuesFromWhere(c.Name, c.GetValue(where, null), "AND");
                                break;
                        }
                }
            }

            return query;
        }

        /// <summary>
        /// Gets the values from where.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="clause">The clause.</param>
        /// <returns>System.String.</returns>
        private static string GetValuesFromWhere(string name, object value, string clause)
        {
            return (name.ToUpper()) switch
            {
                "EMAIL" => $" {clause} email LIKE '%{ value }%'",
                _ => $" {clause} { name }  = { value } "
            };
        }

        /// <summary>
        /// Determines whether [is any null or empty] [the specified object].
        /// </summary>
        /// <param name="obj">object.</param>
        /// <returns><c>true</c> if [is any null or empty] [the specified object]; otherwise, <c>false</c>.</returns>
        private static bool IsAnyNullOrEmpty(object obj)
        {
            return !obj.GetType().GetProperties().Any();
        }
    }
}
