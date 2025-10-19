using LostManagementApp.Dao;
using LostManagementApp.DatabaseContext;
using LostManagementApp.Models;
using NuGet.Protocol.Core.Types;

namespace LostManagementApp.Service
{
    public class LoginService
    {
        private readonly ILoginDao _loginDao;
        public LoginService(ILoginDao LoginDao)
        {
            _loginDao = LoginDao;
        }

        public string? Authenticate(string userId, string password)
        {
            var user = _loginDao.GetUser(userId, password);
            if (user.UserId == -1) return null;

            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            var now = DateTime.UtcNow;
            var expire = now.AddMinutes(30);

            _loginDao.SaveLoginToken(user.UserId, token, now, expire);
            return token;
        }

        public bool IsTokenValid(string token)
        {
            var login = _loginDao.GetLoginByToken(token);
            return login != null && login.ExpireDate > DateTime.UtcNow;
        }
    }
}
