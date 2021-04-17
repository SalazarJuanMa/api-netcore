// ***********************************************************************
// Assembly         :  Domain.Tests
// Author           : jrosas
// Created          : 08-11-2020
//
// Last Modified By : jrosas
// Last Modified On : 08-11-2020
// ***********************************************************************
// <copyright file="SignInResponseTests.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SI.Domain.Entities;
using RandomTestValues;

namespace SI.Domain.Tests.Entities
{
    /// <summary>
    /// Defines test class SignInResponseTests.
    /// </summary>
    [TestClass]
    public class SignInResponseTests
    {
        /// <summary>
        /// The Client
        /// </summary>
        private SignInResponse _signInResponse;

        /// <summary>
        /// Tests the initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _signInResponse = RandomValue.Object<SignInResponse>();
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            _signInResponse = null;
        }

        /// <summary>
        /// Defines the test method SignInResponset_Instance.
        /// </summary>
        [TestMethod]
        public void SignInResponse_Instance()
        {
            Assert.IsNotNull(_signInResponse);
            foreach (var c in _signInResponse.GetType().GetProperties())
            {
                Assert.IsNotNull(c.GetValue(_signInResponse, null));
            }
        }
    }
}
