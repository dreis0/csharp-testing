using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
    public interface IBookingStorage
    {
        IQueryable<Booking> GetActiveBookings(int idToExclude);
    }

    public class BookingStorage : IBookingStorage
    {
        UnitOfWork _unitOfWork;

        public BookingStorage(UnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public IQueryable<Booking> GetActiveBookings(int idToExclude)
        {
            return _unitOfWork
                .Query<Booking>()
                .Where(b => b.Id != idToExclude && b.Status != "Cancelled");

        }

        public class UnitOfWork
        {
            public IQueryable<T> Query<T>()
            {
                return new List<T>().AsQueryable();
            }
        }
    }
}
