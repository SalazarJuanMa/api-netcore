// ***********************************************************************
// Assembly         : Application
// Author           : jrosas
// Created          : 08-10-2020
//
// Last Modified By : jrosas
// Last Modified On : 09-04-2020
// ***********************************************************************
// <copyright file="MappingProfile.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AutoMapper;
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace SI.Application.Common.Mappings
{
    /// <summary>
    /// Class MappingProfile.
    /// Implements the <see cref="Profile" />
    /// </summary>
    /// <seealso cref="Profile" />
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile" /> class.
        /// </summary>
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Applies the mappings from assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
           List<Type> types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (Type type in types)
            {
                object instance = Activator.CreateInstance(type);

                MethodInfo methodInfo = type.GetMethod("Mapping")
                    ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });

            }
        }
    }
}