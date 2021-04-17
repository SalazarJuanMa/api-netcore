// ***********************************************************************
// Assembly         :  Application.Tests
// Author           : jrosas
// Created          : 08-11-2020
//
// Last Modified By : jrosas
// Last Modified On : 08-17-2020
// ***********************************************************************
// <copyright file="MappingTests.cs" company="MS">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SI.Application.Common.Mappings;

namespace  SI.Application.Tests.Common.Mappings
{
    /// <summary>
    /// Defines test class MappingTests.
    /// </summary>
    [TestClass]
    public class MappingTests
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private IConfigurationProvider _configuration;
        /// <summary>
        /// The mapper
        /// </summary>
        private IMapper _mapper;

        /// <summary>
        /// Tests the initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            _configuration = null;
            _mapper = null;
        }

        /// <summary>
        /// Defines the test method ShouldCreateInstance.
        /// </summary>
        [TestMethod]
        public void ShouldCreateInstance()
        {
            MappingProfile mapper = new MappingProfile();
            Assert.IsNotNull(mapper);
        }

        /// <summary>
        /// Shoulds the support mapping from source to destination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        [DataTestMethod]
        [DataRow(typeof(SI.Domain.Entities.Users), typeof(SI.Application.Common.Models.Users))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = Activator.CreateInstance(source);
            _mapper.Map(instance, source, destination);
        }
    }
}
