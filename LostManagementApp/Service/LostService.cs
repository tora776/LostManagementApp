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

        /// <summary>
        /// アプリ起動時・検索処理時に紛失物を取得する
        /// </summary>
        /// <param name="lost">紛失物</param>
        /// <returns></returns>
        public List<Lost> GetLostList(Lost lost)
        {
            return _lostDao.GetLostList(lost);
        }

        /// <summary>
        /// 詳細画面遷移時、紛失物を取得する
        /// </summary>
        /// <param name="LostId">紛失物ID</param>
        /// <returns></returns>
        public Lost GetLost(int LostId)
        {
            return _lostDao.GetLost(LostId);
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
