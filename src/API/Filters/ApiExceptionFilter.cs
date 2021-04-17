// ***********************************************************************
// Assembly         : APP
// Author           : jrosas
// Created          : 08-17-2020
//
// Last Modified By : jrosas
// Last Modified On : 09-10-2020
// ***********************************************************************
// <copyright file="ApiExceptionFilter.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Core.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace APP.Filters
{
    /// <summary>
    /// Class ApiExceptionFilter.
    /// Implements the <see cref="ExceptionFilterAttribute" />
    /// </summary>
    /// <seealso cref="ExceptionFilterAttribute" />
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {

        /// <summary>
        /// The exception handlers
        /// </summary>
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiExceptionFilter" /> class.
        /// </summary>
        public ApiExceptionFilter()
        {
            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(ArgumentException), HandleGenericException },
                { typeof(ArgumentNullException), HandleGenericException },
                { typeof(NullReferenceException), HandleGenericException },
                { typeof(RecordsNotFoundException), HandleRecordsNotFoundException },
                { typeof(OverflowException), HandleGenericException }
            };
        }


        /// <summary>
        /// Called when [exception].
        /// </summary>
        /// <param name="context">The context.</param>
        /// <inheritdoc />
        public override void OnException(ExceptionContext context)
        {
            HandleException(context);
            base.OnException(context);
        }

        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="context">The context.</param>
        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            HandleUnknownException(context);
        }

        /// <summary>
        /// Handles the unknown exception.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void HandleUnknownException(ExceptionContext context)
        {
            ProblemDetails details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request."
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }

        /// <summary>
        /// Handles the validation exception.
        /// </summary>
        /// <param name="context">The context.</param>
        private void HandleValidationException(ExceptionContext context)
        {
            ValidationException exception = context.Exception as ValidationException;
            ValidationProblemDetails details = new ValidationProblemDetails(exception.Errors)
            {
                Status = StatusCodes.Status400BadRequest
            };
            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        /// <summary>
        /// Handles the not found exception.
        /// </summary>
        /// <param name="context">The context.</param>
        private void HandleNotFoundException(ExceptionContext context)
        {
            NotFoundException exception = context.Exception as NotFoundException;
            ProblemDetails details = new ProblemDetails()
            {
                Title = "The specified resource was not found.",
                Detail = exception.Message,
                Status = StatusCodes.Status404NotFound
            };
            context.Result = new NotFoundObjectResult(details);

            context.ExceptionHandled = true;
        }

        /// <summary>
        /// Handles the generic exception.
        /// </summary>
        /// <param name="context">The context.</param>
        private void HandleGenericException(ExceptionContext context)
        {
            ProblemDetails details = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Detail = "The specified resource was not found.",
                Title = "An error occurred while processing your request."
            };
            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status404NotFound
            };

            context.ExceptionHandled = true;
        }
        private void HandleRecordsNotFoundException(ExceptionContext context)
        {
            RecordsNotFoundException exception = context.Exception as RecordsNotFoundException;
            ProblemDetails details = new ProblemDetails()
            {
                Title = "Records not found.",
                Detail = exception.Message,
                Status = StatusCodes.Status204NoContent
            };
            context.Result = new NoContentResult();

            context.ExceptionHandled = true;
        }
    }
}
