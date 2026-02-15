using LostManagementApp.Dao;
using LostManagementApp.DatabaseContext;
using LostManagementApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LostManagementApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly LostContext _context;
        private readonly LoginDao loginDao;

        public LoginController(LostContext LostContext)
        {
            _context = LostContext;
            loginDao = new LoginDao(_context);
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Authenticate([Bind("UserId,Password")] LoginRequest request)
        //public IActionResult Authenticate([FromBody] LoginRequest request)
        {
            try
            {
                var user = loginDao.GetUser(request.UserId, request.Password);

                if (user.UserId == -1)
                {
                    //TODO: ログイン失敗の理由をユーザーに伝える方法を検討する（例：ユーザー名が存在しない、パスワードが間違っているなど）
                    return RedirectToAction("Index", "Login");
                    //return Unauthorized();
                }
                var token = loginDao.Authenticate(user);
                if (token == null)
                {
                    //TODO: ログイン失敗の理由をユーザーに伝える方法を検討する（例：ユーザー名が存在しない、パスワードが間違っているなど）
                    return RedirectToAction("Index", "Login");
                    //return Unauthorized();
                }

                if (!loginDao.IsTokenValid(token))
                {
                    return Unauthorized();
                }

                //return Json(new { token });
                return RedirectToAction("Index", "Home", new { UserId = user.UserId });
            }
            catch (Exception ex)
            {
                // ログ出力などのエラーハンドリングをここで行う

                // ここで例外内容をログに出力
                Console.WriteLine("Authenticate error: " + ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }

        public IActionResult CheckToken([FromBody] TokenRequest request)
        {
            if (!loginDao.IsTokenValid(request.Token))
                return Unauthorized();

            return Ok();
        }
    }

    public class LoginRequest
    {
        public required string UserId { get; set; }
        public required string Password { get; set; }
    }

    public class TokenRequest
    {
        public required string Token { get; set; }
    }
}