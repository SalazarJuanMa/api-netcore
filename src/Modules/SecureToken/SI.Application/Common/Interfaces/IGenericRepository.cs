// ***********************************************************************
// Assembly         : Application
// Author           : jrosas
// Created          : 11-03-2020
//
// Last Modified By : jrosas
// Last Modified On : 11-20-2020
// ***********************************************************************
// <copyright file="IGenericRepository.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SI.Application.Common.Interfaces
{
    /// <summary>
    /// Interface IAsyncGenericRepository
    /// </summary>
    public interface IGenericRepository
    {
        /// <summary>
        /// Generics the get asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">The where.</param>
        /// <param name="clause">The clause.</param>
        /// <returns>Task&lt;IEnumerable&lt;T&gt;&gt;.</returns>
        Task<IEnumerable<T>> GetAsync<T>(object where, string clause = "");
    }
}
