// ***********************************************************************
// Assembly         : Application
// Author           : jrosas
// Created          : 08-17-2020
//
// Last Modified By : jrosas
// Last Modified On : 09-04-2020
// ***********************************************************************
// <copyright file="ValidationException.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Core.Application.Common.Exceptions
{
    /// <summary>
    /// Class ValidationException.
    /// Implements the <see cref="Exception" />
    /// </summary>
    /// <seealso cref="Exception" />
    public class ValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException" /> class.
        /// </summary>
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException" /> class.
        /// </summary>
        /// <param name="failures">The failures.</param>
        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            IEnumerable<IGrouping<string, string>> failureGroups = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage);

            foreach (IGrouping<string, string> failureGroup in failureGroups)
            {
                string propertyName = failureGroup.Key;
                string[] propertyFailures = failureGroup.ToArray();

                Errors.Add(propertyName, propertyFailures);
            }
        }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>The errors.</value>
        public IDictionary<string, string[]> Errors { get; }
    }
}
