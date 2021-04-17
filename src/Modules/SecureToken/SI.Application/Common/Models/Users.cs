// ***********************************************************************
// Assembly         : Application
// Author           : jrosas
// Created          : 8/17/20
//
// Last Modified By : jrosas
// Last Modified On : 09-08-2020
// ***********************************************************************
// <copyright file="Employee.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AutoMapper;
using SI.Application.Common.Mappings;
using System;

namespace SI.Application.Common.Models
{
    /// <summary>
    /// Class Users.
    /// Implements the <see cref="IMapFrom{Domain.Entities.Employee}" />
    /// </summary>
    /// <seealso cref="IMapFrom{Domain.Entities.Employee}" />
    public class Users : IMapFrom<SI.Domain.Entities.Users>
    {
        public Guid Id_User { get; set; }

        public string Email { get; set; }

        public bool Enabled { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        /// <summary>
        /// Mappings the specified profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Users, Users>();
        }
    }

}

