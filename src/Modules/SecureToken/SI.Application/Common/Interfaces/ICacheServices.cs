// ***********************************************************************
// Assembly         : Application
// Author           : jrosas
// Created          : 09-14-2020
//
// Last Modified By : jrosas
// Last Modified On : 09-14-2020
// ***********************************************************************
// <copyright file="ICacheServices.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SI.Application.Common.Interfaces
{
    /// <summary>
    /// Interface ICacheServices
    /// </summary>
    public interface ICacheServices
    {
        /// <summary>
        /// Gets the cache object.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Object.</returns>
        T GetCacheObject<T>(string key);

        /// <summary>
        /// Sets the cache object.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="obj">The object.</param>
        /// <returns>System.Object.</returns>
        T SetCacheObject<T>(string key, T obj);
    }
}
