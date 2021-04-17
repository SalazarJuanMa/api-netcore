// ***********************************************************************
// Assembly         : Application
// Author           : jrosas
// Created          : 08-24-2020
//
// Last Modified By : jrosas
// Last Modified On : 09-08-2020
// ***********************************************************************
// <copyright file="ICurrentUserService.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SI.Domain.Entities;
using System.Threading.Tasks;

namespace  SI.Application.Common.Interfaces
{
    /// <summary>
    /// Interface ICurrentUserService
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="domainName">Name of the domain.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>Task&lt;SignInResponse&gt;.</returns>
        Task<SignInResponse> ValidateUser(string domainName, string username, string password);
    }
}
