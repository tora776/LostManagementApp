using System.Diagnostics;
using LostManagementApp.Dao;
using LostManagementApp.DatabaseContext;
using LostManagementApp.Models;
using LostManagementApp.Service;
using Microsoft.AspNetCore.Mvc;

namespace LostManagementApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LostService _lostService;

        public HomeController(ILogger<HomeController> logger, LostService lostService)
        {
            _logger = logger;
            _lostService = lostService;

        }
        
        public IActionResult Lost()
        {
            // TODO:ユーザーIDを自動取得
            // 紛失物の条件指定はなし
            List<Lost> Losts = _lostService.GetLostList(new Lost
            {
                UserId = 1,
                LostDate = null,
                FoundDate = null,
                LostItem = "",
                LostPlace = "",
                LostDetailedPlace = "",
                User = new Users
                {
                    UserId = 1,
                    UserName = "",
                    Email = "",
                    Password = "",
                    RegistrateDate = DateTime.Now,
                    Losts = new List<Lost>(),
                    Logins = new List<Login>()
                }
            });
            return View(Losts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Detail(int LostId)
        {
            var LostData = _lostService.GetLost(LostId);
            return View(LostData);
        }

        public IActionResult Login()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
