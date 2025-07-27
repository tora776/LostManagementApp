using LostManagementApp.Models;

namespace LostManagementApp.Dao
{
    public interface ILostDao
    {
        List<Lost> GetLostList(Lost lost);
        void InsertLost(Lost lost);
        void UpdateLost(Lost lost);
        void DeleteLost(Lost lost);

        Lost GetLost(int lostId);
    }
}
