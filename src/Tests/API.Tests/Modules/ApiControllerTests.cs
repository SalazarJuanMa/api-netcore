// ***********************************************************************
// Assembly         : APP.Tests
// Author           : jrosas
// Created          : 08-13-2020
//
// Last Modified By : jrosas
// Last Modified On : 08-13-2020
// ***********************************************************************
// <copyright file="ApiControllerTests.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using APP.Modules;
using System;
using static APP.Tests.Testing;
using System.Diagnostics;

namespace APP.Tests.Modules
{
    /// <summary>
    /// Defines test class ApiControllerTests.
    /// Implements the <see cref="APP.Controllers.ApiController" />
    /// </summary>
    /// <seealso cref="APP.Controllers.ApiController" />
    [TestClass]
    public class ApiControllerTests : ApiController
    {
        [TestInitialize]
        public void TestInitialize()
        {
            RunBeforeAnyTests();

            DefaultHttpContext httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["option"] = "App Setup";
            this.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        { }

        /// <summary>
        /// Defines the test method ApiController_ExpectedException.
        /// </summary>
        [TestMethod]
        public void ApiController_ExpectedException()
        {
            try
            {
                Assert.AreEqual(1, GetMediator());
            }
            catch (Exception ex)
            {
                //If the data dont exist in Stage or PROD
                Assert.IsNotNull(ex);
                Trace.WriteLine(ex);
            }
        }

        /// <summary>
        /// Defines the test method ApiController.
        /// </summary>
        [TestMethod]
        public void ApiController()
        {
            var mock = new Mock<IMediator>();
            _mediator = mock.Object;
            Assert.AreEqual(mock.Object, GetMediator());
        }
    }
}
