// ***********************************************************************
// Assembly         : Infrastructure
// Author           : jrosas
// Created          : 08-17-2020
//
// Last Modified By : jrosas
// Last Modified On : 11-09-2020
// ***********************************************************************
// <copyright file="SecureTokenServices.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Core.Application.Common.Constants;
using SI.Application.Common.Interfaces;
using SI.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System;

namespace SI.Infrastructure.services
{
    /// <summary>
    /// Class SecureTokenServices.
    /// Implements the <see cref="ISecureTokenServices" />
    /// </summary>
    /// <seealso cref="ISecureTokenServices" />
    public class SecureTokenServices : ISecureTokenServices
    {
        /// <summary>
        /// The secret
        /// </summary>
        private readonly string _secret;

        /// <summary>
        /// The exp date
        /// </summary>
        private readonly string _expDate;

        /// <summary>
        /// The issuer
        /// </summary>
        private readonly string _issuer;

        /// <summary>
        /// Gets the JWT issuer.
        /// </summary>
        /// <value>The JWT issuer.</value>
        private static string JwtIssuer
        {
            get
            {
                return Environment.GetEnvironmentVariable(MasterModelConstants.EnviromentVarName.JWT_ISSUER);
            }
        }

        /// <summary>
        /// Gets the JWT secret.
        /// </summary>
        /// <value>The JWT secret.</value>
        private static string JwtSecret
        {
            get
            {
                return Environment.GetEnvironmentVariable(MasterModelConstants.EnviromentVarName.JWT_SECRET);
            }
        }

        /// <summary>
        /// Gets the exp date.
        /// </summary>
        /// <value>The exp date.</value>
        private static string ExpDate
        {
            get
            {
                return Environment.GetEnvironmentVariable(MasterModelConstants.EnviromentVarName.EXPIRATION_IN_MINUTES);
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SecureTokenServices" /> class.
        /// </summary>
        public SecureTokenServices()
        {
            _secret = JwtSecret;
            _issuer = JwtIssuer;
            _expDate = ExpDate;
        }

        /// <summary>
        /// Gets the security token.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="identifierId">The identifier identifier.</param>
        /// <returns>Task&lt;JWT&gt;.</returns>
        public async Task<Jwt> GetSecurityToken(string userName, string identifierId)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_secret);
            SecurityTokenDescriptor tokenDescriptor = await SecurityToken(key, userName, identifierId);

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return BuildToken(tokenHandler, token);
        }

        /// <summary>
        /// Securities the token.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="identifierId">The identifier identifier.</param>
        /// <returns>SecurityTokenDescriptor.</returns>
        private async Task<SecurityTokenDescriptor> SecurityToken(byte[] key, string userName, string identifierId)
        {
            return await Task.FromResult<SecurityTokenDescriptor>(new SecurityTokenDescriptor
            {
                Issuer = _issuer,
                Subject = new ClaimsIdentity(new[]
                  {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.NameIdentifier, identifierId)
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_expDate)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });
        }

        /// <summary>
        /// Generates the refresh token.
        /// </summary>
        /// <returns>System.String.</returns>
        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        /// <summary>
        /// Builds the token.
        /// </summary>
        /// <param name="tokenHandler">The token handler.</param>
        /// <param name="token">The token.</param>
        /// <returns>JWT.</returns>
        private static Jwt BuildToken(JwtSecurityTokenHandler tokenHandler, SecurityToken token)
        {
            return new Jwt()
            {
                Authorization = tokenHandler.WriteToken(token),
                Expiration = token.ValidTo,
                CurrentTime = token.ValidFrom,
                RefreshToken = GenerateRefreshToken()
            };
        }
    }
}
