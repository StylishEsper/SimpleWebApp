using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NETD3202_Lab5_V3.Data;
using NETD3202_Lab5_V3.Models;

namespace NETD3202_Lab5_V3.Controllers
{
    public class MoreDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoreDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MoreDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MoreDetails.Include(m => m.gameID);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MoreDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View(nameof(UnknownID));
            }

            var moreDetail = await _context.MoreDetails
                .Include(m => m.gameID)
                .FirstOrDefaultAsync(m => m.gID == id);
            if (moreDetail == null)
            {
                return View(nameof(UnknownID));
            }

            return View(moreDetail);
        }

        // GET: MoreDetails/Create
        public IActionResult Create()
        {
            ViewData["gID"] = new SelectList(_context.VideoGames, "gameID", "gameID");
            return View();
        }

        // POST: MoreDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("gID,description")] MoreDetail moreDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moreDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["gID"] = new SelectList(_context.VideoGames, "gameID", "gameID", moreDetail.gID);
            return View(moreDetail);
        }

        // GET: MoreDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moreDetail = await _context.MoreDetails.FindAsync(id);
            if (moreDetail == null)
            {
                return NotFound();
            }
            ViewData["gID"] = new SelectList(_context.VideoGames, "gameID", "gameID", moreDetail.gID);
            return View(moreDetail);
        }

        // POST: MoreDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("gID,description")] MoreDetail moreDetail)
        {
            if (id != moreDetail.gID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moreDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoreDetailExists(moreDetail.gID))
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
            ViewData["gID"] = new SelectList(_context.VideoGames, "gameID", "gameID", moreDetail.gID);
            return View(moreDetail);
        }

        // GET: MoreDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moreDetail = await _context.MoreDetails
                .Include(m => m.gameID)
                .FirstOrDefaultAsync(m => m.gID == id);
            if (moreDetail == null)
            {
                return NotFound();
            }

            return View(moreDetail);
        }

        // POST: MoreDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moreDetail = await _context.MoreDetails.FindAsync(id);
            _context.MoreDetails.Remove(moreDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //UnknownID Page - Displays an error message
        public IActionResult UnknownID()
        {
            return View();
        }

        private bool MoreDetailExists(int id)
        {
            return _context.MoreDetails.Any(e => e.gID == id);
        }
    }
}
