﻿using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class ErrorLoggerTests
    {
        ErrorLogger _logger;

        [SetUp]
        public void SetUp()
        {
            _logger = new ErrorLogger();
        }

        /* Testing a void type method */

        [Test]
        public void Log_WhenCalled_SetTheLastErrorProperty()
        {
            string testString = "test";
            _logger.Log("test");

            Assert.That(_logger.LastError, Is.EqualTo(testString));
        }

        /* Testing exception throwing */

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_InvalidError_ThrowsArgumentNullException(string error)
        {
            Assert.That(() => _logger.Log(error), Throws.ArgumentNullException);
            //For exceptions that are not a property in "Throws" use:
            Assert.That(() => _logger.Log(error), Throws.Exception.TypeOf<ArgumentNullException>());
        }
    }
}
