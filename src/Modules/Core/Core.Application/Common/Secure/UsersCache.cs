// ***********************************************************************
// Assembly         : Core.Application
// Author           : jrosas
// Created          : 04-05-2021
//
// Last Modified By : jrosas
// Last Modified On : 04-05-2021
// ***********************************************************************
// <copyright file="Users.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Core.Application.Common.Secure
{
    /// <summary>
    /// Class Users.
    /// </summary>
    public class UsersCache
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        /// <value>The secret.</value>
        public string Secret { get; set; }
    }
}
