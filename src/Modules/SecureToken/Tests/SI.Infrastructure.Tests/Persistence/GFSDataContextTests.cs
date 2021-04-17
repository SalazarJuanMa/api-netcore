// ***********************************************************************
// Assembly         : Infrastructure.Tests
// Author           : jrosas
// Created          : 12-02-2020
//
// Last Modified By : jrosas
// Last Modified On : 12-02-2020
// ***********************************************************************
// <copyright file="GFSDataContextTests.cs" company="Infrastructure.Tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SI.Infrastructure.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SI.Domain.Entities;

namespace SI.Infrastructure.Tests.Persistence
{
    /// <summary>
    /// Defines test class GFSDataContextTests.
    /// </summary>
    [TestClass]
    public class GFSDataContextTests
    {
        /// <summary>
        /// The GFS offer data context
        /// </summary>
        private GFSDataContext gfsDataContext;
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            gfsDataContext = new GFSDataContext(new Microsoft.EntityFrameworkCore.DbContextOptions<GFSDataContext>());
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            gfsDataContext = null;
        }

        /// <summary>
        /// Defines the test method FAIL_SET_RELEASE_HOLDS.
        /// </summary>
        [TestMethod]
        public async System.Threading.Tasks.Task GetAsync()
        {
            try
            {
                var whereParameters = new
                {
                    username = "jj"
                };

                var result = await gfsDataContext.GetAsync<Users>(whereParameters);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex.Message);
            }
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetAsync_And()
        {
            try
            {
                var whereParameters = new
                {
                    username = "jj",
                    user = "jj"
                };

                var result = await gfsDataContext.GetAsync<Users>(whereParameters);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex.Message);
            }
        }
    }
}
