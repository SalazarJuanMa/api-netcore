// ***********************************************************************
// Assembly         : Application
// Author           : jrosas
// Created          : 08-13-2020
//
// Last Modified By : jrosas
// Last Modified On : 09-04-2020
// ***********************************************************************
// <copyright file="UnhandledExceptionBehaviour.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Core.Application.Common.Constants;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Common.Behaviors
{
    /// <summary>
    /// Class UnhandledExceptionBehaviour.
    /// Implements the <see cref="IPipelineBehavior{TRequest, TResponse}" />
    /// </summary>
    /// <typeparam name="TRequest">The type of the t request.</typeparam>
    /// <typeparam name="TResponse">The type of the t response.</typeparam>
    /// <seealso cref="IPipelineBehavior{TRequest, TResponse}" />
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<TRequest> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnhandledExceptionBehaviour{TRequest, TResponse}" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="next">The next.</param>
        /// <returns>Task&lt;TResponse&gt;.</returns>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            string requestName = typeof(TRequest).Name;
            string req = JsonConvert.SerializeObject(request);

            try
            {
                if (requestName.ToUpper().Contains(MasterModelConstants.SECURE_TOKEN))
                {
                    _logger.LogInformation($"INFO : {requestName}");
                }
                else
                {
                    _logger.LogInformation($"INFO: {requestName} : {req}");
                }

                return await next();
            }
            catch (Exception ex)
            {
                if (requestName.ToUpper().Contains(MasterModelConstants.SECURE_TOKEN))
                {
                    _logger.LogError($"ERROR : {ex} ", ExceptionConstants.Message.HANDLE_EXCEPTION + $"{requestName}");
                }
                else
                {
                    _logger.LogError($"ERROR : {ex} ", ExceptionConstants.Message.HANDLE_EXCEPTION + $"{requestName} : {req}");
                }

                throw;
            }
        }
    }
}
