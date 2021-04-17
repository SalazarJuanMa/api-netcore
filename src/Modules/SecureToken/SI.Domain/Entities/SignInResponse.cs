// ***********************************************************************
// Assembly         : Domain
// Author           : jrosas
// Created          : 09-08-2020
//
// Last Modified By : jrosas
// Last Modified On : 11-20-2020
// ***********************************************************************
// <copyright file="SignInResponse.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SI.Domain.Entities
{
    /// <summary>
    /// Class SignInResponse.
    /// </summary>
    public class SignInResponse
    {
        /// <summary>
        /// Gets or sets the identifier identifier.
        /// </summary>
        /// <value>The identifier identifier.</value>
        public string IdentifierID { get; set; }
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>The employee identifier.</value>
        public int? EmployeeID { get; set; }
    }
}
