using NUnit.Framework;
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
    }
}
