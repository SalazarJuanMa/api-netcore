// ***********************************************************************
// Assembly         : Application
// Author           : jrosas
// Created          : 08-17-2020
//
// Last Modified By : jrosas
// Last Modified On : 09-04-2020
// ***********************************************************************
// <copyright file="SecureTokenRequest.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using Newtonsoft.Json;

namespace SI.Application.SecureToken.Queries
{
    /// <summary>
    /// Class SecureTokenRequest.
    /// Implements the <see cref="MediatR.IRequest{Queries.SecureTokenResponse}" />
    /// </summary>
    /// <seealso cref="MediatR.IRequest{Queries.SecureTokenResponse}" />
    public class SecureTokenRequest : IRequest<SecureTokenResponse>
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        /// <value>The domain.</value>
        [JsonIgnore]
        public string Domain { get; set; }
    }
}
