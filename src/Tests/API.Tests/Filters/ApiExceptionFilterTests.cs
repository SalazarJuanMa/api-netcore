// ***********************************************************************
// Assembly         : APP.Tests
// Author           : jrosas
// Created          : 08-17-2020
//
// Last Modified By : jrosas
// Last Modified On : 03-02-2021
// ***********************************************************************
// <copyright file="ApiExceptionFilterTests.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Core.Application.Common.Exceptions;
using APP.Filters;
using System;
using System.Collections.Generic;

namespace APP.Tests.Filters
{
    /// <summary>
    /// Defines test class ApiExceptionFilterTests.
    /// </summary>
    [TestClass]
    public class ApiExceptionFilterTests
    {

        /// <summary>
        /// The APP.exception filter
        /// </summary>
        private ApiExceptionFilter _apiExceptionFilter;

        /// <summary>
        /// The action context
        /// </summary>
        private ActionContext _actionContext;

        /// <summary>
        /// Tests the initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _apiExceptionFilter = new ApiExceptionFilter();
            _actionContext = new ActionContext()
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            _apiExceptionFilter = null;
            _actionContext = null;
        }

        /// <summary>
        /// Defines the test method ApiExceptionFilter.
        /// </summary>
        [TestMethod]
        public void ApiExceptionFilter()
        {
            Assert.IsNotNull(_apiExceptionFilter);
        }

        /// <summary>
        /// Defines the test method ApiExceptionFilter_OnException.
        /// </summary>
        [TestMethod]
        public void ApiExceptionFilter_OnException()
        {
            var mockException = new Mock<Exception>();

            mockException.Setup(e => e.StackTrace)
              .Returns("Test stacktrace");
            mockException.Setup(e => e.Message)
              .Returns("Test message");
            mockException.Setup(e => e.Source)
              .Returns("Test source");

            var exceptionContext = new ExceptionContext(_actionContext, new List<IFilterMetadata>())
            {
                Exception = mockException.Object
            };

            _apiExceptionFilter.OnException(exceptionContext);
            Assert.IsNotNull(_apiExceptionFilter);
        }

        /// <summary>
        /// Defines the test method ApiExceptionFilter_ValidationException.
        /// </summary>
        [TestMethod]
        public void ApiExceptionFilter_ValidationException()
        {
            var validationException = new ValidationException();

            var exceptionContext = new ExceptionContext(
                _actionContext,
                new List<IFilterMetadata>())
            {
                Exception = validationException
            };

            _apiExceptionFilter.OnException(exceptionContext);
            Assert.IsNotNull(_apiExceptionFilter);
        }


        /// <summary>
        /// Defines the test method ApiExceptionFilter_NotFoundException.
        /// </summary>
        [TestMethod]
        public void ApiExceptionFilter_NotFoundException()
        {
            var notFoundException = new NotFoundException();

            var exceptionContext = new ExceptionContext(
                _actionContext,
                new List<IFilterMetadata>())
            {
                Exception = notFoundException
            };

            _apiExceptionFilter.OnException(exceptionContext);
            Assert.IsNotNull(_apiExceptionFilter);
        }

        /// <summary>
        /// Defines the test method ApiExceptionFilter_ArgumentException.
        /// </summary>
        [TestMethod]
        public void ApiExceptionFilter_ArgumentException()
        {
            var notFoundException = new ArgumentException();

            var exceptionContext = new ExceptionContext(
                _actionContext,
                new List<IFilterMetadata>())
            {
                Exception = notFoundException
            };

            _apiExceptionFilter.OnException(exceptionContext);
            Assert.IsNotNull(_apiExceptionFilter);
        }

        /// <summary>
        /// Defines the test method ApiExceptionFilter_RecordsNotFoundException.
        /// </summary>
        [TestMethod]
        public void ApiExceptionFilter_RecordsNotFoundException()
        {
            var notFoundException = new RecordsNotFoundException(1);

            var exceptionContext = new ExceptionContext(
                _actionContext,
                new List<IFilterMetadata>())
            {
                Exception = notFoundException
            };

            _apiExceptionFilter.OnException(exceptionContext);
            Assert.IsNotNull(_apiExceptionFilter);
        }
    }
}
