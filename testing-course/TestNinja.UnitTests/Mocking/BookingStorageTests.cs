using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class BookingStorageTests
    {
        Mock<IBookingStorage> _storage;
        Booking _booking;

        [SetUp]
        public void SetUp()
        {
            _storage = new Mock<IBookingStorage>();
            _booking = new Booking
            {
                ArrivalDate = new DateTime(2020, 01, 01, 08, 00, 00),
                DepartureDate = new DateTime(2020, 01, 01, 10, 00, 00),
                Id = 1,
            };
        }

        [Test]
        public void OverlappingBookingExists_NoOverlapp_ReturnsEmpty()
        {
            _storage
                .Setup(st => st.GetActiveBookings(1))
                .Returns(new List<Booking>
                {
                    new Booking
                    {
                        Id = 2,
                        ArrivalDate = new DateTime(2020, 01, 01, 14, 00, 00),
                        DepartureDate = new DateTime(2020, 01, 01, 16, 00, 00),
                    }
                }.AsQueryable());

            var result = BookingHelper
                .OverlappingBookingsExist(_booking, _storage.Object);

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void OverlappingBookingExists_HasOverlapp_ReturnsReference()
        {
            _storage
                .Setup(st => st.GetActiveBookings(1))
                .Returns(new List<Booking>
                {
                    new Booking
                    {
                        Id = 2,
                        ArrivalDate = new DateTime(2020, 01, 01, 09, 00, 00),
                        DepartureDate = new DateTime(2020, 01, 01, 11, 00, 00),
                        Reference = "Overlapp"
                    }
                }.AsQueryable());

            var result = BookingHelper
                .OverlappingBookingsExist(_booking, _storage.Object);

            Assert.That(result, Is.EqualTo("Overlapp"));
        }

        [Test]
        public void OverlappingBookingExists_CancelledStatus_Returnsempty()
        {
            var result = BookingHelper
                .OverlappingBookingsExist(new Booking { Status = "Cancelled" });

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void OverlappingBookingExists_ValidStatus_SearchBooking()
        {
            BookingHelper
                .OverlappingBookingsExist(new Booking { Id = 1 }, _storage.Object);

            _storage.Verify(st => st.GetActiveBookings(1));
        }
    }
}
