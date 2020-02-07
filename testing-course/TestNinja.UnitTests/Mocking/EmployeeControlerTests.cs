using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class EmployeeControlerTests
    {
        Mock<IEmployeeStorage> _storage;
        EmployeeController _controller;

        [SetUp]
        public void SetUp()
        {
            _storage = new Mock<IEmployeeStorage>();
            _controller = new EmployeeController(_storage.Object);
        }

        [Test]
        public void DeleteEmployee_ValidId_DeletesEmployee()
        {
            _controller.DeleteEmployee(1);
            _storage.Verify(s => s.RemoveEmployee(1));
        }

        [Test]
        public void DeleteEmployee_ValidId_ReturnsRedirectObject()
        {
            var result = _controller.DeleteEmployee(1);
            Assert.That(result, Is.TypeOf<RedirectResult>());
        }

        [Test]
        public void DeleteEmployee_InvalidId_ReturnsEmpty()
        {
            _storage.Setup(st => st.RemoveEmployee(1)).Callback(() => { return; });

            var result = _controller.DeleteEmployee(1);

            Assert.That(result, Is.Not.TypeOf<ActionResult>());
        }
    }
}
