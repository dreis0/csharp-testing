using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    public interface IHouseKeeperRepository
    {
        IQueryable<Housekeeper> GetHousekeepers();
    }
    public class HousekeeperRepository : IHouseKeeperRepository
    {
        private readonly UnitOfWork _unitOfWork;

        public HousekeeperRepository()
        {
            _unitOfWork = new UnitOfWork();
        }

        public IQueryable<Housekeeper> GetHousekeepers()
        {
            return _unitOfWork.Query<Housekeeper>();
        }
    }

    public class UnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }
}
