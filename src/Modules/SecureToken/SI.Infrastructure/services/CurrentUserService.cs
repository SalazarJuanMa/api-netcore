// ***********************************************************************
// Assembly         : Infrastructure
// Author           : jrosas
// Created          : 08-24-2020
//
// Last Modified By : jrosas
// Last Modified On : 03-30-2021
// ***********************************************************************
// <copyright file="CurrentUserService.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Core.Application.Common.Constants;
using Core.Application.Common.Exceptions;
using SI.Application.Common.Interfaces;
using SI.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Core.Application.Common.Crypto;
using Core.Application.Common.Secure;

namespace SI.Infrastructure.services
{
    /// <summary>
    /// Class CurrentUserService.
    /// Implements the <see cref="ICurrentUserService" />
    /// </summary>
    /// <seealso cref="ICurrentUserService" />
    public class CurrentUserService : ICurrentUserService
    {
        /// <summary>
        /// The database context
        /// </summary>
        private static IGFSDbContext _dbContext;

        /// <summary>
        /// The cache service
        /// </summary>
        private static ICacheServices _cacheService;

        /// <summary>
        /// Gets the JWT secret.
        /// </summary>
        /// <value>The JWT secret.</value>
        private static string Secret
        {
            get
            {
                return Environment.GetEnvironmentVariable(MasterModelConstants.EnviromentVarName.JWT_SECRET);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentUserService" /> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="cacheService">The cache service.</param>
        public CurrentUserService(IGFSDbContext dbContext, ICacheServices cacheService)
        {
            _dbContext = dbContext;
            _cacheService = cacheService;
        }

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="domainName">Name of the domain.</param>
        /// <param name="userName">The user name.</param>
        /// <param name="password">The password.</param>
        /// <returns>Task&lt;SignInResponse&gt;.</returns>
        public async Task<SignInResponse> ValidateUser(string domainName, string userName, string password)
        {
            try
            {
                var whereParameters = new
                {
                    email = userName
                };

                IEnumerable<Users> employee = await _dbContext.GetAsync<Users>(whereParameters);
                Users emp = employee != null ? employee.FirstOrDefault() : new Users();

                BuildCache(emp.Id_User.ToString(), userName, password);
                return await Task.FromResult<SignInResponse>(new SignInResponse()
                {
                    IdentifierID = emp.Id_User.ToString(),
                    UserName = userName,
                });

            }
            catch (Exception ex)
            {
                throw new NotFoundException(ExceptionConstants.Message.SECURE_TOKEN_EXCEPTION, ex);
            }
        }

        private static void BuildCache(string id, string userName, string password)
        {
            dynamic userCache = _cacheService.GetCacheObject<object>(id);
            if (userCache == null)
            {
                var objEmployee = new UsersCache
                {
                    UserName = userName,
                    Secret = AES.OpenSSLEncrypt(password, Secret)
                };

                _cacheService.SetCacheObject(id.ToString(), objEmployee);
            }
        }
    }
}
