// ***********************************************************************
// Assembly         : Infrastructure
// Author           : jrosas
// Created          : 09-14-2020
//
// Last Modified By : jrosas
// Last Modified On : 03-30-2021
// ***********************************************************************
// <copyright file="CacheServices.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SI.Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using Core.Application.Common.Constants;

namespace SI.Infrastructure.services
{
    /// <summary>
    /// Class CacheServices.
    /// Implements the <see cref="ICacheServices" />
    /// </summary>
    /// <seealso cref="ICacheServices" />
    public class CacheServices : ICacheServices
    {
        /// <summary>
        /// Gets the expiration in minutes.
        /// </summary>
        /// <value>The expiration in minutes.</value>
        private static string ExpirationInMinutes
        {
            get
            {
                return Environment.GetEnvironmentVariable(MasterModelConstants.EnviromentVarName.EXPIRATION_IN_MINUTES);
            }
        }

        /// <summary>
        /// The cache
        /// </summary>
        private static IMemoryCache _cache;

        /// <summary>
        /// The options
        /// </summary>
        private static MemoryCacheEntryOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheServices" /> class.
        /// </summary>
        /// <param name="cache">The cache.</param>
        public CacheServices(IMemoryCache cache)
        {
            _cache = cache;
            _options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(Convert.ToInt64(ExpirationInMinutes)), // cache will expire in 45 minutes
                SlidingExpiration = TimeSpan.FromMinutes(Convert.ToInt64(ExpirationInMinutes)) // cache will expire if inactive for 30 minutes
            };
        }

        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns>System.Object.</returns>
        public T GetCacheObject<T>(string key)
        {
            return (T)_cache.Get(key);
        }

        /// <summary>
        /// Sets the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="obj">The object.</param>
        /// <returns>System.Object.</returns>
        public T SetCacheObject<T>(string key, T obj)
        {
            T o = (T)_cache.Get(key);
            if (o != null)
            {
                return o;
            }

            return _cache.Set(key, obj, _options);
        }
    }
}
