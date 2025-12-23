using LostManagementApp.Models;
using LostManagementApp.ViewModels;

namespace LostManagementApp.Dao
{
    public interface ILostDao
    {
        // 全件取得
        List<Lost> GetLostList(LostViewModel model);
        void InsertLost(LostViewModel model);
        void UpdateLost(Lost lost);
        void DeleteLostIds(List<int> lostId);
        void DeleteLost(int lostId);

        // 検索処理
        Lost GetLost(int lostId);
    }
}
