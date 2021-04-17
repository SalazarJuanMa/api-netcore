// ***********************************************************************
// Assembly         :  Domain.Tests
// Author           : jrosas
// Created          : 08-11-2020
//
// Last Modified By : jrosas
// Last Modified On : 08-11-2020
// ***********************************************************************
// <copyright file="JWTResponseTests.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SI.Application.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomTestValues;

namespace Application.Tests.Common.Models
{
    /// <summary>
    /// Defines test class JWTResponseTests.
    /// </summary>
    [TestClass]
    public class JWTResponseTests
    {
        /// <summary>
        /// The JWTResponseTests
        /// </summary>
        private JwtResponse _jwtResponse;

        /// <summary>
        /// Tests the initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _jwtResponse = RandomValue.Object<JwtResponse>();
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            _jwtResponse = null;
        }

        /// <summary>
        /// Defines the test JWTResponse_Instance.
        /// </summary>
        [TestMethod]
        public void JWTResponse_Instance()
        {
            Assert.IsNotNull(_jwtResponse);
            foreach (var c in _jwtResponse.GetType().GetProperties())
            {
                Assert.IsNotNull(c.GetValue(_jwtResponse, null));
            }
        }
    }
}
