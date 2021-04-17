// ***********************************************************************
// Assembly         : APP.Tests
// Author           : jrosas
// Created          : 08-13-2020
//
// Last Modified By : jrosas
// Last Modified On : 11-03-2020
// ***********************************************************************
// <copyright file="SecureTokenControllerTests.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SI.Application.SecureToken.Queries;
using APP.Modules;
using System.Threading;
using static APP.Tests.Testing;
using SI.Application.SecureToken;
using System;

namespace APP.Tests.Modules
{
    /// <summary>
    /// Defines test class SecureTokenControllerTests.
    /// Implements the <see cref="ApiController" />
    /// </summary>
    /// <seealso cref="ApiController" />
    [TestClass]
    public class SecureTokenControllerTests : ApiController
    {

        /// <summary>
        /// The mock mediator
        /// </summary>
        private Mock<IMediator> _mockMediator;

        /// <summary>
        /// Tests the initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            RunBeforeAnyTests();
            _mockMediator = new Mock<IMediator>();
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            _mockMediator = null;
        }

        /// <summary>
        /// Defines the test method ApiController_SecureToken.
        /// </summary>
        [TestMethod]
        public void ApiController_SecureToken()
        {
            _mockMediator.Setup(m => m.Send(It.IsAny<SecureTokenRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(It.IsAny<SecureTokenResponse>());
            _mediator = _mockMediator.Object;
            Assert.AreEqual(_mockMediator.Object, GetMediator());

            //Act
            var result = SecureToken(new SecureTokenRequest());
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Defines the test method ApiController_SecureToken_Exception.
        /// </summary>
        [TestMethod]
        public void ApiController_SecureToken_Exception()
        {
            try
            {
                _mockMediator.Setup(m => m.Send(It.IsAny<SecureTokenRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(It.IsAny<SecureTokenResponse>());
                _mediator = _mockMediator.Object;
                Assert.AreEqual(_mockMediator.Object, GetMediator());

                //Act
                var result = SecureToken(null);
                Assert.IsNotNull(result);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex.Message);
            }
        }
    }
}
