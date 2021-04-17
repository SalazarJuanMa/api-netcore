// ***********************************************************************
// Assembly         : Application
// Author           : jrosas
// Created          : 08-17-2020
//
// Last Modified By : jrosas
// Last Modified On : 09-11-2020
// ***********************************************************************
// <copyright file="SecureTokenResponse.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;

namespace SI.Application.SecureToken
{
    /// <summary>
    /// Class SecureTokenResponse.
    /// </summary>
    public class SecureTokenResponse
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets the server transaction identifier.
        /// </summary>
        /// <value>The server transaction identifier.</value>
        public string Authorization { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>The employee identifier.</value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        /// <value>The refresh token.</value>
        [JsonIgnore]
        public string RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the current time.
        /// </summary>
        /// <value>The current time.</value>
        [JsonIgnore]
        public DateTime CurrentTime { get; set; }
        /// <summary>
        /// Gets or sets the expiration.
        /// </summary>
        /// <value>The expiration.</value>
        [JsonIgnore]
        public DateTime Expiration { get; set; }
    }
}
