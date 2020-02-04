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
        public void CalculateDemeritPoints_InvalidSpeed_ThrowsException()
        {
            Assert.That(
                    () => _pointsCalculator.CalculateDemeritPoints(-1), 
                    Throws.TypeOf<ArgumentOutOfRangeException>()
                );
        }

        [Test]
        public void CalculateDemeritPoints_SpeedBellowLimit_Returns0()
        {
            var result = _pointsCalculator.CalculateDemeritPoints(60);
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CalculateDemeritPoints_SpeedAboveLimit_ReturnsDemeritPoints()
        {
            var result = _pointsCalculator.CalculateDemeritPoints(85);
            Assert.That(result, Is.EqualTo(4));
        }
    }
}
