using LostManagementApp.Models;
using Microsoft.AspNetCore.Mvc;
using LostManagementApp.DatabaseContext;

namespace LostManagementApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly LostContext _context;

        public LoginController(LostContext LostContext)
        {
            _context = LostContext;
        }
        /*
        // [HttpGet("index")]
        public IActionResult Index()
        {
            return View("~/Views/Login/Index.cshtml");
            //return View("Login");
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginRequest request)
        {
            try
            {
                var token = _context.Authenticate(request.UserId, request.Password);
                if (token == null)
                {
                    return Unauthorized();
                }

                return Json(new { token });
            }
            catch (Exception ex)
            {
                // ログ出力などのエラーハンドリングをここで行う

                // ここで例外内容をログに出力
                Console.WriteLine("Authenticate error: " + ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("checktoken")]
        public IActionResult CheckToken([FromBody] TokenRequest request)
        {
            if (!_context.IsTokenValid(request.Token))
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
        */
    }
}