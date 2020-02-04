using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class CustomControllerTests
    {
        CustomerController _controller;

        [SetUp]
        public void SetUp()
        {
            _controller = new CustomerController();
        }

        /* Testing the return type of a method */

        [Test]
        public void GetCustomer_IdIsZero_ReturnsNotFound()
        {
            var result = _controller.GetCustomer(0);

            Assert.That(result, Is.TypeOf<NotFound>()); //Must be NotFound object
            //Or
            Assert.That(result, Is.InstanceOf<NotFound>()); //Must be NotFound object or a derivative
        }

        [Test]
        public void GetCustomer_IdIsNotZero_ReturnsOk()
        {
            var result = _controller.GetCustomer(1);
            Assert.That(result, Is.TypeOf<Ok>());
        }
    }
}
