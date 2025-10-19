using LostManagementApp.Models;

namespace LostManagementApp.Dao
{
    public interface ILoginDao
    {
        // 全件取得
        Users GetUser(string userName, string password);
        void SaveLoginToken(int userId, string token, DateTime loginDate, DateTime expireDate);
        Login? GetLoginByToken(string token);
        void UpdateTokenExpireDate(string token, DateTime newExpireDate);
    }
}
