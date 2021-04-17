// ***********************************************************************
// Assembly         : Application
// Author           : jrosas
// Created          : 08-17-2020
//
// Last Modified By : jrosas
// Last Modified On : 09-04-2020
// ***********************************************************************
// <copyright file="ISecureTokenServices.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SI.Domain.Entities;
using System.Threading.Tasks;

namespace  SI.Application.Common.Interfaces
{
    /// <summary>
    /// Interface ISecureTokenServices
    /// </summary>
    public interface ISecureTokenServices
    {
        /// <summary>
        /// Gets the security token.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="identifierId">The identifier identifier.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        Task<Jwt> GetSecurityToken(string userName, string identifierId);
    }
}
