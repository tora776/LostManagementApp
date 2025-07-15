using LostManagementApp.Dao;
using LostManagementApp.DatabaseContext;
using LostManagementApp.Models;

namespace LostManagementApp.Service
{
    public class LostService
    {
        private readonly ILostDao _lostDao;
        public LostService(ILostDao lostDao)
        {
            _lostDao = lostDao;
        }

        public List<Lost> GetLost(Lost lost)
        {
            return _lostDao.GetLost(lost);
        }

        public void InsertLost(Lost lost)
        {
            _lostDao.InsertLost(lost);
        }

        public void UpdateLost(Lost lost)
        {
            _lostDao.UpdateLost(lost);
        }

        public void DeleteLost(Lost lost)
        {
            _lostDao.DeleteLost(lost);
        }
    }
}
