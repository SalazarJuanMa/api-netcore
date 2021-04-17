// ***********************************************************************
// Assembly         :  Domain.Tests
// Author           : jrosas
// Created          : 08-11-2020
//
// Last Modified By : jrosas
// Last Modified On : 08-11-2020
// ***********************************************************************
// <copyright file="JWTTests.cs" company="MS">
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
    /// Defines test class JWTTests.
    /// </summary>
    [TestClass]
    public class JWTTests
    {
        /// <summary>
        /// The JWT
        /// </summary>
        private Jwt _jwt;

        /// <summary>
        /// Tests the initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _jwt = RandomValue.Object<Jwt>();
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            _jwt = null;
        }

        /// <summary>
        /// Defines the test method JWT_Instance.
        /// </summary>
        [TestMethod]
        public void JWT_Instance()
        {
            Assert.IsNotNull(_jwt);
            foreach (var c in _jwt.GetType().GetProperties())
            {
                Assert.IsNotNull(c.GetValue(_jwt, null));
            }
        }
    }
}
