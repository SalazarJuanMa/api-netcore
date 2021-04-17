// ***********************************************************************
// Assembly         : Domain
// Author           : jrosas
// Created          : 09-08-2020
//
// Last Modified By : jrosas
// Last Modified On : 09-08-2020
// ***********************************************************************
// <copyright file="Employee.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel.DataAnnotations;

namespace SI.Domain.Entities
{
    /// <summary>
    /// Class Employee.
    /// </summary>
    public class Users
    {
        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>The employee identifier.</value>
        [Key]
        public Guid Id_User { get; set; }

        public string Email { get; set; }

        public bool Enabled { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

    }

}
