using LostManagementApp.Dao;
using LostManagementApp.DatabaseContext;
using LostManagementApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Diagnostics;

namespace LostManagementApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LostContext _context;
        private readonly LostDao lostDao;

        public HomeController(ILogger<HomeController> logger, LostContext context)
        {
            _logger = logger;
            _context = context;
            lostDao = new LostDao(_context);
        }

        // GET: Losts
        [HttpGet]
        public IActionResult Index()
        {
            //TODO:ユーザーIDを取得する
            LostDto lostDto = new LostDto
            {
                UserId = 1,
                LostDate = null,
                FoundDate = null,
                LostItem = "",
                LostPlace = "",
                LostDetailedPlace = ""
            };
            var list = lostDao.GetLostList(lostDto);
            ViewData["SearchModel"] = new LostDto();
            return View(list);
        }

        // POST: Losts (検索フォーム)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("UserId,LostDate,FoundDate,LostItem,LostPlace,LostDetailedPlace")] LostDto search)
        {
            var list = lostDao.GetLostList(search);
            ViewData["SearchModel"] = search;
            return View(list);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var lost = lostDao.GetLost(id);
            if (lost == null) return NotFound();

            return View(lost);
        }

        // POST: Losts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Insert(LostDto lostDto)
        {
            if (ModelState.IsValid)
            {
                lostDao.InsertLost(lostDto);
            }
            return RedirectToAction("Index");
            //return View(lost);
        }

        // POST: Losts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([Bind("LostId,UserId,IsFound,LostDate,FoundDate,LostItem,LostPlace,LostDetailedPlace,RegistrateDate,UpdateDate")] Lost lost)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO:userIdを自動取得
                    lost.UserId = 1;
                    lostDao.UpdateLost(lost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LostExists(lost.LostId)) return NotFound();
                    else throw;
                }
                return RedirectToAction("Detail", new { id = lost.LostId});
            }
            // TODO:失敗時の処理
            return RedirectToAction("Detail", new { id = lost.LostId });
        }

        // POST: Losts/DeleteSelected (複数削除)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSelected([FromForm] int[] selectedIds)
        {
            if (selectedIds != null && selectedIds.Length > 0)
            {
                lostDao.DeleteLostIds(selectedIds.ToList());
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Losts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            lostDao.DeleteLost(id);
            return RedirectToAction(nameof(Index));
        }
        
        private bool LostExists(int id)
        {
            return _context.Lost.Any(e => e.LostId == id);
        }
        
    }
}
