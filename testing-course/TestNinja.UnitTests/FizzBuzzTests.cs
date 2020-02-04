using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class FizzBuzzTests
    {
        private readonly string FIZZ = "Fizz";
        private readonly string BUZZ = "Buzz";

        [Test]
        public void GetOutput_IsDividedBy3_ReturnsFizz()
        {
            var result = FizzBuzz.GetOutput(6);
            Assert.That(result, Is.EqualTo(FIZZ));
        }

        [Test]
        public void GetOutput_IsDividedBy5_RetursBuzz()
        {
            var result = FizzBuzz.GetOutput(10);
            Assert.That(result, Is.EqualTo(BUZZ));
        }

        [Test]
        public void GetOutput_IsDividedByBoth_ReturnsFizzBuzz()
        {
            var result = FizzBuzz.GetOutput(15);
            Assert.That(result, Is.EqualTo($"{FIZZ}{BUZZ}"));
        }

        [Test]
        public void GetOutput_IsNotDividedByNeither_ReturnsNumber()
        {
            var result = FizzBuzz.GetOutput(8);
            Assert.That(result, Is.EqualTo(8.ToString()));
        }
    }
}
