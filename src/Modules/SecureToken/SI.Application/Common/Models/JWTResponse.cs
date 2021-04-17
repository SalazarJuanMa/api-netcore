// ***********************************************************************
// Assembly         : Application
// Author           : jrosas
// Created          : 09-14-2020
//
// Last Modified By : jrosas
// Last Modified On : 09-14-2020
// ***********************************************************************
// <copyright file="JWTResponse.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SI.Application.Common.Mappings;
using AutoMapper;
using System;

namespace SI.Application.Common.Models
{
    /// <summary>
    /// Class JWTResponse.
    /// Implements the <see cref="Application.Common.Mappings.IMapFrom{Domain.Entities.JWT}" />
    /// </summary>
    /// <seealso cref="Application.Common.Mappings.IMapFrom{Domain.Entities.JWT}" />
    public class JwtResponse : IMapFrom<Domain.Entities.Jwt>
    {
        /// <summary>
        /// Gets or sets the sec session token.
        /// </summary>
        /// <value>The sec session token.</value>
        public string Authorization { get; set; }
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

        /// <summary>
        /// Mappings the specified profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Jwt, JwtResponse>();
        }
    }
}
