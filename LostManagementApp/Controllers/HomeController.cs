using LostManagementApp.Dao;
using LostManagementApp.DatabaseContext;
using LostManagementApp.Models;
using LostManagementApp.ViewModels;
using LostManagementApp.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
            //LostDto lostDto = new LostDto
            LostViewModel model = new LostViewModel
            {
                UserId = 1,
                LostDate = null,
                FoundDate = null,
                LostItem = "",
                LostPlace = "",
                LostDetailedPlace = ""
            };
            var list = lostDao.GetLostList(model);
            ViewData["SearchModel"] = new LostDto();
            return View(list);
        }

        // POST: Losts (検索フォーム)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("UserId,LostDate,FoundDate,LostItem,LostPlace,LostDetailedPlace")] LostViewModel model)
        {
            var list = lostDao.GetLostList(model);
            ViewData["SearchModel"] = model;
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
        public IActionResult Insert(LostViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.LostDate.HasValue)
                {
                    if (!DateTime.TryParse(model.LostDate.ToString(), out DateTime lostDate))
                    {
                        ModelState.AddModelError("LostDate", "紛失日には正しい日付を入力してください。");
                    }
                }

                if (model.FoundDate.HasValue)
                {
                    if (!DateTime.TryParse(model.FoundDate.ToString(), out DateTime FoundDate))
                    {
                        ModelState.AddModelError("FoundDate", "紛失日には正しい日付を入力してください。");
                    }
                }

                if (!String.IsNullOrEmpty(model.LostItem))
                {
                    if (model.LostItem.Length > 100)
                    {
                        ModelState.AddModelError("LostItem", "紛失物は100文字以内で入力してください。");
                    }
                }

                if (!String.IsNullOrEmpty(model.LostPlace))
                {
                    if (model.LostPlace.Length > 100)
                    {
                        ModelState.AddModelError("LostPlace", "紛失物は100文字以内で入力してください。");
                    }
                }

                if (!String.IsNullOrEmpty(model.LostDetailedPlace))
                {
                    if (model.LostDetailedPlace.Length > 100)
                    {
                        ModelState.AddModelError("LostDetailedPlace", "紛失物は100文字以内で入力してください。");
                    }
                }

                if (ModelState.IsValid)
                {
                    
                    lostDao.InsertLost(model);
                }
            }
            return RedirectToAction("Index", model);
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
