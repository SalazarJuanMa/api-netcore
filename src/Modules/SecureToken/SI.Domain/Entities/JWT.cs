// ***********************************************************************
// Assembly         : Domain
// Author           : jrosas
// Created          : 09-14-2020
//
// Last Modified By : jrosas
// Last Modified On : 09-14-2020
// ***********************************************************************
// <copyright file="JWT.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace SI.Domain.Entities
{
    /// <summary>
    /// Class JWT.
    /// </summary>
    public class Jwt
    {
        /// <summary>
        /// Gets or sets the server transaction identifier.
        /// </summary>
        /// <value>The server transaction identifier.</value>
        public string Authorization { get; set; }

        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        /// <value>The refresh token.</value>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the current time.
        /// </summary>
        /// <value>The current time.</value>
        public DateTime CurrentTime { get; set; }

        /// <summary>
        /// Gets or sets the expiration.
        /// </summary>
        /// <value>The expiration.</value>
        public DateTime Expiration { get; set; }
    }
}
