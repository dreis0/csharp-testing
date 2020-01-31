using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestClass]
    public class ReservationTests
    {
        [TestMethod]
        public void CanBeCacelledBy_IsAdmin_ReturnsTrue()
        {
            var reservation = new Reservation();
            var adminUser = new User { IsAdmin = true };

            bool result = reservation.CanBeCancelledBy(adminUser);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanBeCacelledBy_IsNotAdmin_ReturnsFalse()
        {
            var reservation = new Reservation();
            var adminUser = new User { IsAdmin = false };

            bool result = reservation.CanBeCancelledBy(adminUser);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CanBeCacelledBy_IsCreator_ReturnsTrue()
        {
            var user = new User { IsAdmin = false };
            var reservation = new Reservation { MadeBy = user };

            bool result = reservation.CanBeCancelledBy(user);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanBeCacelledBy_IsNotCreator_ReturnsFalse()
        {
            var reservation = new Reservation();
            var user = new User { IsAdmin = false };

            bool result = reservation.CanBeCancelledBy(user);

            Assert.IsFalse(result);
        }
    }
}
