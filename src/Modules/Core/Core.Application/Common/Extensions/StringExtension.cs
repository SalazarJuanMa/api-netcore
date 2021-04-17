// ***********************************************************************
// Assembly         : Application
// Author           : jrosas
// Created          : 10-29-2020
//
// Last Modified By : jrosas
// Last Modified On : 10-29-2020
// ***********************************************************************
// <copyright file="StringExtension.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Text.RegularExpressions;

namespace Core.Application.Common.Extensions
{
    /// <summary>
    /// Class StringExtension.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Converts to hexstring.
        /// </summary>
        /// <param name="byteArray">The byte array.</param>
        /// <returns>System.String.</returns>
        public static string ToHexString(this byte[] byteArray)
        {
            return "0x" + string.Concat(Array.ConvertAll(byteArray, x => x.ToString("X2")));
        }

        /// <summary>
        /// Sentence case extension
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToSentenceCase(this string str)
        {
            return Regex.Replace(str, "[a-z][A-Z]", m => $"{m.Value[0]} {m.Value[1]}");
        }
        /// <summary>
        /// Strips out all non-number characters.
        /// </summary>
        /// <param name="str">The str.</param>
        /// <returns>A string.</returns>
        public static string NumericOnly(this string str)
        {
            return Regex.Replace(str, "[^0-9.]", "");
        }
    }
}
