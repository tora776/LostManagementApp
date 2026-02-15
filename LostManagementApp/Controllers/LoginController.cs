using LostManagementApp.Dao;
using LostManagementApp.DatabaseContext;
using LostManagementApp.ViewModels;
using LostManagementApp.Utils;
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
            if (TempData["ErrorMessage"] != null)
            {
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Authenticate([Bind("UserId,Password")] LoginViewModel model)
        {
            try
            {
                var user = loginDao.GetUser(model.UserId, model.Password);

                if (user.UserId == -1)
                {
                    TempData["ErrorMessage"] = ErrorMessages.MSG_006;
                    return RedirectToAction("Index", "Login");
                }
                var token = loginDao.Authenticate(user);
                if (token == null)
                {
                    TempData["ErrorMessage"] = ErrorMessages.MSG_006;
                    return RedirectToAction("Index", "Login");
                }

                if (!loginDao.IsTokenValid(token))
                {
                    return Unauthorized();
                }

                //return Json(new { token });
                // TODO:トークンを渡して認証状態を管理したほうがよい？
                //return RedirectToAction("Index", "Home", new { UserId = user.UserId });
                return RedirectToAction("Index", "Home", new { Token = token });
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

    //public class LoginRequest
    //{
    //    public required string UserId { get; set; }
    //    public required string Password { get; set; }
    //}

    public class TokenRequest
    {
        public required string Token { get; set; }
    }
}