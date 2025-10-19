using LostManagementApp.DatabaseContext;
using LostManagementApp.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace LostManagementApp.Dao
{
    // LoginDao クラスが ILoginDao インターフェースを実装するように修正
    public class LoginDao : ILoginDao
    {
        private readonly LostContext _context;

        public LoginDao(LostContext LostContext)
        {
            _context = LostContext;
        }

        /// <summary>
        /// ユーザーIDとパスワードでユーザーを取得
        /// </summary>
        public Users GetUser(string userName, string password)
        {
            try
            {
                var user = _context.Users
                                   .Where(x => x.UserName == userName && x.Password == password)
                                   .FirstOrDefault()
                ?? new Users { UserId = -1,
                              UserName = "aaa",
                              Email = "test@gmail.com",
                              Password = "dummy"};

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving user: " + ex.Message);
                return new Users
                {
                    UserId = -1,
                    UserName = "aaa",
                    Email = "test@gmail.com",
                    Password = "dummy"
                };
            }
        }

        /// <summary>
        /// トークンをDBに保存
        /// </summary>
        public void SaveLoginToken(int userId, string token, DateTime loginDate, DateTime expireDate)
        {
            try
            {
                _context.Login.Add(new Login
                {
                    UserId = userId,
                    Token = token,
                    LoginDate = loginDate,
                    ExpireDate = expireDate
                });
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving login token: " + ex.Message);
            }
        }

        /// <summary>
        /// トークンでログイン情報を取得
        /// </summary>
        public Login GetLoginByToken(string token)
        {
            try
            {
                var login = _context.Login
                                    .Where(x => x.Token == token)
                                    .FirstOrDefault()
                            ?? new Login
                            {
                                LoginId = -1,
                                UserId = -1,
                                Token = "dummy"
                            };

                return login;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving login by token: " + ex.Message);
                return new Login
                {
                    LoginId = -1,
                    UserId = -1,
                    Token = "dummy"
                };
            }
        }

        /// <summary>
        /// トークンの有効期限を更新（必要に応じて）
        /// </summary>
        public void UpdateTokenExpireDate(string token, DateTime newExpireDate)
        {
            try
            {
                var login = _context.Login
                                    .Where(x => x.Token == token)
                                    .FirstOrDefault()
                                    ?? new Login
                                    {
                                        LoginId = -1,
                                        UserId = -1,
                                        Token = "dummy"
                                    };
                if(login.LoginId == -1)
                {
                    Console.WriteLine("トークンが見つかりませんでした: " + token);
                    return;
                }

                login.ExpireDate = newExpireDate;

                _context.Login
                        .Update(login);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating token expire date: " + ex.Message);
            }
        }
    }
}
