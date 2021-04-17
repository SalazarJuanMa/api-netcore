// ***********************************************************************
// Assembly         : Infrastructure.Tests
// Author           : jrosas
// Created          : 12-02-2020
//
// Last Modified By : jrosas
// Last Modified On : 12-02-2020
// ***********************************************************************
// <copyright file="GenericMethodsTests.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SI.Infrastructure.Persistence.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SI.Infrastructure.Tests.Persistence.Helpers
{
    /// <summary>
    /// Defines test class GenericMethodsTests.
    /// </summary>
    [TestClass]
    public class GenericMethodsTests
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
        }

        /// <summary>
        /// Defines the test method BuildWhere_Exception.
        /// </summary>
        [TestMethod]
        public void BuildWhere_Exception()
        {
            try
            {
                GenericMethods.BuildWhere(null);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex.Message);
            }
        }


        /// <summary>
        /// Defines the test method BuildWhere_User_Name.
        /// </summary>
        [TestMethod]
        public void BuildWhere_User_Name()
        {
            object UserName = new object();
            var whereParameters = new
            {
                UserName
            };

            var query = GenericMethods.BuildWhere(whereParameters);
            Assert.IsNotNull(query);
        }
    }
}
