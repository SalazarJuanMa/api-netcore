// ***********************************************************************
// Assembly         : Application
// Author           : jrosas
// Created          : 08-13-2020
//
// Last Modified By : jrosas
// Last Modified On : 09-04-2020
// ***********************************************************************
// <copyright file="ValidationBehavior.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Common.Behaviors
{
    /// <summary>
    /// Class ValidationBehavior.
    /// Implements the <see cref="IPipelineBehavior{TRequest, TResponse}" />
    /// </summary>
    /// <typeparam name="TRequest">The type of the t request.</typeparam>
    /// <typeparam name="TResponse">The type of the t response.</typeparam>
    /// <seealso cref="IPipelineBehavior{TRequest, TResponse}" />
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// The validators
        /// </summary>
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationBehavior{TRequest, TResponse}" /> class.
        /// </summary>
        /// <param name="validators">The validators.</param>
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="next">The next.</param>
        /// <returns>Task&lt;TResponse&gt;.</returns>
        /// <exception cref="Exceptions.ValidationException"></exception>
        /// <exception cref="ValidationException"></exception>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                ValidationContext context = new ValidationContext(request);

                FluentValidation.Results.ValidationResult[] validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                List<FluentValidation.Results.ValidationFailure> failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                    throw new Exceptions.ValidationException(failures);
            }
            return await next();
        }
    }
}