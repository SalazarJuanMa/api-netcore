// ***********************************************************************
// Assembly         : Application
// Author           : jrosas
// Created          : 08-10-2020
//
// Last Modified By : jrosas
// Last Modified On : 09-04-2020
// ***********************************************************************
// <copyright file="IMapFrom.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AutoMapper;
using System.Diagnostics.CodeAnalysis;

namespace SI.Application.Common.Mappings
{
    /// <summary>
    /// Interface IMapFrom
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMapFrom<T>
    {
        /// <summary>
        /// Mappings the specified profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        [ExcludeFromCodeCoverage]
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
