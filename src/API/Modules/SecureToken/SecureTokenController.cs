// ***********************************************************************
// Assembly         : APP
// Author           : jrosas
// Created          : 08-17-2020
//
// Last Modified By : jrosas
// Last Modified On : 11-03-2020
// ***********************************************************************
// <copyright file="SecureTokenController.cs" company="PK">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Net;
using Core.Application.Common.Constants;
using Core.Application.Common.Exceptions;
using SI.Application.SecureToken.Queries;
using SI.Application.SecureToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using APP.Constants;
using Swashbuckle.AspNetCore.Filters;

namespace APP.Modules
{
    /// <summary>
    /// Class ApiController.
    /// Implements the <see cref="ControllerBase" />
    /// </summary>
    /// <seealso cref="ControllerBase" />
    public partial class ApiController
    {

        /// <summary>
        /// Determines whether [is sec token valid] [the specified get authentication query].
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/SecureToken
        ///     This will return a SecureTokenResponse object
        /// </remarks>
        /// <param name="getAuthenticationQuery">The get authentication query.</param>
        /// <returns>SecureTokenResponse.</returns>
        /// <exception cref="NotFoundException"></exception>
        /// <response code="202">Returns the newly created item</response>
        /// <response code="400">Returns bad request</response>
        /// <response code="404">Returns Not Found</response>
        [HttpPost]
        [Route("SecureToken")]
        [SwaggerOperation(OperationId = "SecureToken", Tags = new[] {  "Secure Token"  })]
        [SwaggerResponseHeader(new int[] { 202, 400, 404 }, StartupConstants.Swagger.ResponseHeader.LOCATION_NAME, StartupConstants.Swagger.ResponseHeader.STRING_TYPE, StartupConstants.Swagger.ResponseHeader.LOCATION_DESCRIPTION)]
        [SwaggerResponseHeader(new int[] { 202, 400, 404 }, StartupConstants.Swagger.ResponseHeader.DATE_NAME, StartupConstants.Swagger.ResponseHeader.STRING_TYPE, StartupConstants.Swagger.ResponseHeader.DATE_DESCRIPTION)]
        [SwaggerResponseHeader(new int[] { 202, 400, 404 }, StartupConstants.Swagger.ResponseHeader.CONTENT_TYPE, StartupConstants.Swagger.ResponseHeader.STRING_TYPE, StartupConstants.Swagger.ResponseHeader.CONTENT_DESCRIPTION)]
        [ProducesResponseType(typeof(SecureTokenResponse), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SecureTokenResponse>> SecureToken([FromBody] SecureTokenRequest getAuthenticationQuery)
        {
            if (getAuthenticationQuery == null)
            {
                throw new NotFoundException(ExceptionConstants.Message.REQUEST_EXCEPTION);
            }

            return await GetMediator().Send(getAuthenticationQuery);
        }
    }
}
