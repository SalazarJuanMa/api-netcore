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
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException" /> class.
        /// </summary>
        public NotFoundException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException" /> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="key">The key.</param>
        public NotFoundException(string name, object key)
            : base($"Error: The {name} with the ID {key} was not found.")
        {
        }
    }
}
