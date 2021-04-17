// ***********************************************************************
// Assembly         : Application
// Author           : jrosas
// Created          : 08-17-2020
//
// Last Modified By : jrosas
// Last Modified On : 09-04-2020
// ***********************************************************************
// <copyright file="NotFoundException.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace Core.Application.Common.Exceptions
{
    /// <summary>
    /// Class NotFoundException.
    /// Implements the <see cref="Exception" />
    /// </summary>
    /// <seealso cref="Exception" />
    public class RecordsNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordsNotFoundException" /> class.
        /// </summary>
        public RecordsNotFoundException(int batchId)
            : base($"The batch or offer {batchId} does not contain information.")
        {
        }
    }
}
