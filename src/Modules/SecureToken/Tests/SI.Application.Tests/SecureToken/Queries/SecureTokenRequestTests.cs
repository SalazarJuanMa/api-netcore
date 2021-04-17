// ***********************************************************************
// Assembly         :  Application.Tests
// Author           : jrosas
// Created          : 08-13-2020
//
// Last Modified By : jrosas
// Last Modified On : 08-17-2020
// ***********************************************************************
// <copyright file="SecureTokenRequestTests.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SI.Application.SecureToken.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;

namespace SI.Application.Tests.SecureToken.Queries
{
    /// <summary>
    /// Defines test class SecureTokenRequestTests.
    /// </summary>
    [TestClass]
    public class SecureTokenRequestTests
    {
        /// <summary>
        /// The SecureTokenResponse
        /// </summary>
        private SecureTokenRequest _secureTokenRequest;

        /// <summary>
        /// Tests the initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _secureTokenRequest = RandomValue.Object<SecureTokenRequest>();
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
        /// Defines the test method SecureTokenResponse_Instance.
        /// </summary>
        [TestMethod]
        public void SecureTokenResponse_Instance()
        {
            Assert.IsNotNull(_secureTokenRequest);
            foreach (var c in _secureTokenRequest.GetType().GetProperties())
            {
                Assert.IsNotNull(c.GetValue(_secureTokenRequest, null));
            }
        }
    }
}