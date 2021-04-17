// ***********************************************************************
// Assembly         :  Application.Tests
// Author           : jrosas
// Created          : 08-13-2020
//
// Last Modified By : jrosas
// Last Modified On : 08-17-2020
// ***********************************************************************
// <copyright file="SecureTokenResponseTests.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SI.Application.SecureToken;
using RandomTestValues;

namespace SI.Application.Tests.SecureToken
{
    /// <summary>
    /// Defines test class SecureTokenResponseTests.
    /// </summary>
    [TestClass]
    public class SecureTokenResponseTests
    {
        /// <summary>
        /// The SecureTokenResponse
        /// </summary>
        private SecureTokenResponse _secureTokenResponse;

        /// <summary>
        /// Tests the initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _secureTokenResponse = new SecureTokenResponse();
            AddValues();
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            _secureTokenResponse = null;
        }

        /// <summary>
        /// Defines the test method SecureTokenResponse_Instance.
        /// </summary>
        [TestMethod]
        public void SecureTokenResponse_Instance()
        {
            Assert.IsNotNull(_secureTokenResponse);
            foreach (var c in _secureTokenResponse.GetType().GetProperties())
            {
                Assert.IsNotNull(c.GetValue(_secureTokenResponse, null));
            }
        }

        /// <summary>
        /// Adds the values.
        /// </summary>
        private void AddValues()
        {
            _secureTokenResponse = RandomValue.Object<SecureTokenResponse>();
        }
    }
}