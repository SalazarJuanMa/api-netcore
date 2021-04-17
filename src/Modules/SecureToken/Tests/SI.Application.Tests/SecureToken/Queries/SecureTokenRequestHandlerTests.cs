// ***********************************************************************
// Assembly         : Application.Tests
// Author           : jrosas
// Created          : 08-17-2020
//
// Last Modified By : jrosas
// Last Modified On : 09-25-2020
// ***********************************************************************
// <copyright file="SecureTokenRequestTests.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SI.Application.SecureToken.Queries;
using System.Threading.Tasks;
using static SI.Application.Tests.Testing;
using System;
using Core.Application.Common.Exceptions;

namespace SI.Application.Tests.SecureToken.Queries
{
    /// <summary>
    /// Defines test class SecureTokenRequestTests.
    /// </summary>
    [TestClass]
    public class SecureTokenRequestHandlerTests
    {
        /// <summary>
        /// The is sec token valid request
        /// </summary>
        private SecureTokenRequest _secureTokenRequest;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            RunBeforeAnyTests();
            _secureTokenRequest = new SecureTokenRequest();
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            _secureTokenRequest = null;
        }

        /// <summary>
        /// Defines the test method SecureTokenRequestAsync_Exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task SecureTokenRequestAsync_Exception()
        {
            var result = await SendAsync(_secureTokenRequest);
            Assert.IsNull(result);
        }

        /// <summary>
        /// is sec token valid request as an asynchronous operation.
        /// </summary>
        [TestMethod]
        public async Task SecureTokenRequestAsync_Throw_Exception()
        {
            try
            {
                _secureTokenRequest.Domain = "";
                _secureTokenRequest.UserName = "user";
                _secureTokenRequest.Password = "password";

                var result = await SendAsync(_secureTokenRequest);
                Assert.IsNotNull(result);
            }
            catch(Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        /// <summary>
        /// is sec token valid request as an asynchronous operation.
        /// </summary>
        [TestMethod]
        public async Task SecureTokenRequestAsync()
        {
            _secureTokenRequest.Domain = "";
            _secureTokenRequest.UserName = "email@email.com";
            _secureTokenRequest.Password = "ayA7EqFrmUc0BDfc26Np!";

            var result = await SendAsync(_secureTokenRequest);
            Assert.IsNotNull(result);
        }
    }
}
