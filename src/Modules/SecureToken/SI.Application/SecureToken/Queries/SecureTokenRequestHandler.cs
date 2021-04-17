// ***********************************************************************
// Assembly         : Application
// Author           : jrosas
// Created          : 11-02-2020
//
// Last Modified By : jrosas
// Last Modified On : 12-28-2020
// ***********************************************************************
// <copyright file="SecureTokenRequestHandler.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Core.Application.Common.Constants;
using Core.Application.Common.Exceptions;
using SI.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SI.Application.SecureToken.Queries
{
    /// <summary>
    /// Class SecureTokenRequestHandler.
    /// Implements the <see cref="MediatR.IRequestHandler{Queries.SecureTokenRequest, Queries.SecureTokenResponse}" />
    /// </summary>
    /// <seealso cref="MediatR.IRequestHandler{Queries.SecureTokenRequest, Queries.SecureTokenResponse}" />
    public class SecureTokenRequestHandler : IRequestHandler<SecureTokenRequest, SecureTokenResponse>
    {
        /// <summary>
        /// The authentication
        /// </summary>
        private readonly ISecureTokenServices _authentication;

        /// <summary>
        /// The current user service
        /// </summary>
        private readonly ICurrentUserService _currentUserService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecureTokenRequestHandler" /> class.
        /// </summary>
        /// <param name="authentication">The authentication.</param>
        /// <param name="currentUserService">The current user service.</param>
        public SecureTokenRequestHandler(ISecureTokenServices authentication, ICurrentUserService currentUserService)
        {
            _authentication = authentication;
            _currentUserService = currentUserService;
        }

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;SecureTokenResponse&gt;.</returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<SecureTokenResponse> Handle(SecureTokenRequest request, CancellationToken cancellationToken)
        {
            SI.Domain.Entities.SignInResponse resultUser = await _currentUserService.ValidateUser(request.Domain, request.UserName, request.Password);

            if (string.IsNullOrEmpty(resultUser.IdentifierID))
            {
                throw new NotFoundException(ExceptionConstants.Message.SECURE_TOKEN_EXCEPTION);
            }

            SI.Domain.Entities.Jwt resultToken = await _authentication.GetSecurityToken(request.UserName, resultUser.IdentifierID);

            return new SecureTokenResponse
            {
                Message = MasterModelConstants.SUCCESS,
                Authorization = resultToken.Authorization,
                UserName = resultUser.UserName,
                UserId = resultUser.IdentifierID,
                RefreshToken = resultToken.RefreshToken,
                CurrentTime = resultToken.CurrentTime,
                Expiration = resultToken.Expiration
            };
        }
    }
}
