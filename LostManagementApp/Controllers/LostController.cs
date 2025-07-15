using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LostManagementApp.DatabaseContext;
using LostManagementApp.Models;

namespace LostManagementApp.Controllers
{
    public class LostController : Controller
    {
        private readonly LostContext _context;

        public LostController(LostContext context)
        {
            _context = context;
        }

        // GET: Losts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Lost.ToListAsync());
        }

        // GET: Losts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lost = await _context.Lost
                .FirstOrDefaultAsync(m => m.LostId == id);
            if (lost == null)
            {
                return NotFound();
            }

            return View(lost);
        }

        // GET: Losts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Losts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            if (id == null)
            {
                return NotFound();
            }

            var lost = await _context.Lost.FindAsync(id);
            if (lost == null)
            {
                return NotFound();
            }
            return View(lost);
        }

        // POST: Losts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LostId,UserId,IsFound,LostDate,FoundDate,LostItem,LostPlace,LostDetailedPlace,RegistrateDate,UpdateDate")] Lost lost)
        {
            if (id != lost.LostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LostExists(lost.LostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(lost);
        }

        // GET: Losts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lost = await _context.Lost
                .FirstOrDefaultAsync(m => m.LostId == id);
            if (lost == null)
            {
                return NotFound();
            }

            return View(lost);
        }

        // POST: Losts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lost = await _context.Lost.FindAsync(id);
            if (lost != null)
            {
                _context.Lost.Remove(lost);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LostExists(int id)
        {
            return _context.Lost.Any(e => e.LostId == id);
        }
    }
}
