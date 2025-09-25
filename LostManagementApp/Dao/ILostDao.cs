using LostManagementApp.Models;

namespace LostManagementApp.Dao
{
    public interface ILostDao
    {
        // 全件取得
        List<Lost> GetLostList(Lost lost);
        void InsertLost(Lost lost);
        void UpdateLost(Lost lost);
        void DeleteLostIds(List<int> lostId);
        void DeleteLost(int lostId);

        // 検索処理
        Lost GetLost(int lostId);
    }
}
