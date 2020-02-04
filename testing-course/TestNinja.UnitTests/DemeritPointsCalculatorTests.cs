using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class DemeritPointsCalculatorTests
    {
        public DemeritPointsCalculator _pointsCalculator { get; set; }

        [SetUp]
        public void SetUp()
        {
            _pointsCalculator = new DemeritPointsCalculator();
        }

        [Test]
        [TestCase(-1)]
        [TestCase(301)]
        public void CalculateDemeritPoints_InvalidSpeed_ThrowsException(int speed)
        {
            Assert.That(
                    () => _pointsCalculator.CalculateDemeritPoints(speed),
                    Throws.TypeOf<ArgumentOutOfRangeException>()
                );
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(1, 0)]
        [TestCase(65, 0)]
        [TestCase(75, 2)]
        [TestCase(85, 4)]
        public void CalculateDemeritPoints_WhenCalled_ReturnsPoints(int speed, int points)
        {
            var result = _pointsCalculator.CalculateDemeritPoints(speed);
            Assert.That(result, Is.EqualTo(points));
        }
    }
}
