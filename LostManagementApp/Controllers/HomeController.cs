using LostManagementApp.Dao;
using LostManagementApp.DatabaseContext;
using LostManagementApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index()
        {
            LostDto lostDto = new LostDto
            {
                UserId = 1,
                LostDate = null,
                FoundDate = null,
                LostItem = "",
                LostPlace = "",
                LostDetailedPlace = ""
            };
            //TODO:ユーザーIDを取得し、検索を実行
            var list = lostDao.GetLostList(lostDto);
            ViewData["SearchModel"] = new LostDto();
            return View(list);
        }

        // POST: Losts (検索フォーム)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("UserId,LostDate,FoundDate,LostItem,LostPlace,LostDetailedPlace")] LostDto search)
        {
            var query = _context.Lost.AsQueryable();

            // UserId が 0 の場合は全ユーザー（必要なら必須に変更）
            if (search.UserId != 0)
                query = query.Where(x => x.UserId == search.UserId);

            if (search.LostDate.HasValue)
            {
                var d = search.LostDate.Value.Date;
                query = query.Where(x => x.LostDate.HasValue && x.LostDate.Value.Date == d);
            }

            if (search.FoundDate.HasValue)
            {
                var d = search.FoundDate.Value.Date;
                query = query.Where(x => x.FoundDate.HasValue && x.FoundDate.Value.Date == d);
            }

            if (!string.IsNullOrWhiteSpace(search.LostItem))
                query = query.Where(x => x.LostItem != null && x.LostItem.Contains(search.LostItem));

            if (!string.IsNullOrWhiteSpace(search.LostPlace))
                query = query.Where(x => x.LostPlace != null && x.LostPlace.Contains(search.LostPlace));

            if (!string.IsNullOrWhiteSpace(search.LostDetailedPlace))
                query = query.Where(x => x.LostDetailedPlace != null && x.LostDetailedPlace.Contains(search.LostDetailedPlace));

            var list = await query.OrderBy(x => x.LostId).ToListAsync();
            ViewData["SearchModel"] = search;
            return View(list);
        }

        // 検索結果の個別詳細は GET 詳細ページへ（シンプルに GET リンクで遷移）
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();

            var lost = await _context.Lost.FirstOrDefaultAsync(m => m.LostId == id);
            if (lost == null) return NotFound();

            return View(lost);
        }

        // GET: Losts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Losts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LostId,UserId,IsFound,LostDate,FoundDate,LostItem,LostPlace,LostDetailedPlace,RegistrateDate,UpdateDate")] Lost lost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lost);
        }

        // GET: Losts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var lost = await _context.Lost.FindAsync(id);
            if (lost == null) return NotFound();
            return View(lost);
        }

        // POST: Losts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LostId,UserId,IsFound,LostDate,FoundDate,LostItem,LostPlace,LostDetailedPlace,RegistrateDate,UpdateDate")] Lost lost)
        {
            if (id != lost.LostId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LostExists(lost.LostId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(lost);
        }

        // POST: Losts/DeleteSelected (複数削除)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSelected([FromForm] int[] selectedIds)
        {
            if (selectedIds != null && selectedIds.Length > 0)
            {
                var items = _context.Lost.Where(x => selectedIds.Contains(x.LostId));
                _context.Lost.RemoveRange(items);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Losts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var lost = await _context.Lost.FirstOrDefaultAsync(m => m.LostId == id);
            if (lost == null) return NotFound();

            return View(lost);
        }

        // POST: Losts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lost = await _context.Lost.FindAsync(id);
            if (lost != null) _context.Lost.Remove(lost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LostExists(int id)
        {
            return _context.Lost.Any(e => e.LostId == id);
        }
        /*
        public IActionResult Lost()
        {
            // TODO:ユーザーIDを自動取得
            // 紛失物の条件指定はなし
            List<Lost> Losts = _context.GetLostList(new Lost
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
            var LostData = _context.GetLost(LostId);
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
        */
    }
}
