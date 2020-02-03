using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class MathTests
    {
        Math _math;

        [SetUp]
        public void SetUp()
        {
            _math = new Math();
        }

        [Test]
        public void Add_ReturnsSum()
        {
            var result = _math.Add(1, 2);
            Assert.AreEqual(result, 3);
        }

        [Test]
        [TestCase(1, 2, 2)]
        [TestCase(2, 1, 2)]
        [TestCase(1, 1, 1)]
        public void Max_WhenCalled_ReturnsGreaterArgument(int a, int b, int expectedValue)
        {
            var result = _math.Max(a, b);
            Assert.AreEqual(result, expectedValue);
        }

        [Test]
        public void GetOddNumbers_LimitGreaterThen0_ReturnsOddNumbers()
        {
            var result = _math.GetOddNumbers(5);

            //Most general
            Assert.That(result, Is.Not.Empty);

            //Specific
            Assert.That(((int[])result).Length, Is.EqualTo(3));

            //Most specific
            Assert.That(result, Does.Contain(1));
            Assert.That(result, Does.Contain(3));
            Assert.That(result, Does.Contain(5));

            //Or
            Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));
        }

        [Test]
        public void GetOddNumbers_LimitIs0_ReturnsEmpty()
        {
            var result = _math.GetOddNumbers(0);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetOddNumbers_LimitIsLessThen0_ThrowsException()
        {
            var result = _math.GetOddNumbers(-1);
            Assert.That(result, Is.Empty);
        }
    }
}
