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
            List<Lost> Losts = _lostService.GetLost(new Lost
            {
                UserId = 1,
                LostItem = "",
                LostPlace = "",
                LostDetailedPlace = ""
            });
            return View(Losts);
        }

        public IActionResult Privacy()
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
