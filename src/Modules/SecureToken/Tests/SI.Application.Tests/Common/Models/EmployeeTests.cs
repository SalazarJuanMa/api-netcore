// ***********************************************************************
// Assembly         :  Domain.Tests
// Author           : jrosas
// Created          : 08-11-2020
//
// Last Modified By : jrosas
// Last Modified On : 08-11-2020
// ***********************************************************************
// <copyright file="EmployeeTests.cs" company="MS">
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
    /// Defines test class EmployeeTests.
    /// </summary>
    [TestClass]
    public class EmployeeTests
    {
        /// <summary>
        /// The Employee
        /// </summary>
        private Users _employee;

        /// <summary>
        /// Tests the initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _employee = RandomValue.Object<Users>();
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            _employee = null;
        }

        /// <summary>
        /// Defines the test Employee_Instance.
        /// </summary>
        [TestMethod]
        public void Employee_Instance()
        {
            Assert.IsNotNull(_employee);
            foreach (var c in _employee.GetType().GetProperties())
            {
                Assert.IsNotNull(c.GetValue(_employee, null));
            }
        }
    }
}
