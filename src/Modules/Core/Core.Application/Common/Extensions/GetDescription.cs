// ***********************************************************************
// Assembly         : Application
// Author           : jrosas
// Created          : 09-15-2020
//
// Last Modified By : jrosas
// Last Modified On : 09-15-2020
// ***********************************************************************
// <copyright file="GetDescription.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;

namespace Core.Application.Common.Extensions
{
    /// <summary>
    /// Class GetDescription.
    /// </summary>
    public static class GetDescription
    {
        /// <summary>
        /// Gets the specified enum value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue">The enum value.</param>
        /// <returns>System.String.</returns>
        public static string Get<T>(this T enumValue)
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                return null;

            string description = enumValue.ToString();
            System.Reflection.FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                object[] attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return description;
        }
    }
}