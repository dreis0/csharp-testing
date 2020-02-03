using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class HtmlFormatterTests
    {
        HtmlFormatter _formatter;

        [SetUp]
        public void SetUp()
        {
            _formatter = new HtmlFormatter();
        }
        public void FormatAsBold_WhenCalled_EncloseTheStringWithStrongElement()
        {
            string testString = "test";
            var result = _formatter.FormatAsBold(testString);

            //Specific
            Assert.That(result, Is.EqualTo($"<strong>{testString}</strong>"));

            //More general
            Assert.That(result, Does.StartWith("<strong>"));
            Assert.That(result, Does.EndWith("</strong>"));
            Assert.That(result, Does.Contain(testString));
        }
    }
}
