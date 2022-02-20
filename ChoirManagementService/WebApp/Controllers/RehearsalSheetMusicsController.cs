using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize(Roles = "admin")]
    public class RehearsalSheetMusicsController : Controller
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public RehearsalSheetMusicsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: RehearsalSheetMusics
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.RehearsalSheetMusics.Include(r => r.Rehearsal).Include(r => r.SheetMusic);
            return View(await appDbContext.ToListAsync());
        }

        // GET: RehearsalSheetMusics/Details/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rehearsalSheetMusic = await _context.RehearsalSheetMusics
                .Include(r => r.Rehearsal)
                .Include(r => r.SheetMusic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rehearsalSheetMusic == null)
            {
                return NotFound();
            }

            return View(rehearsalSheetMusic);
        }

        // GET: RehearsalSheetMusics/Create
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            ViewData["RehearsalId"] = new SelectList(_context.Rehearsals, "Id", "Location");
            ViewData["SheetMusicId"] = new SelectList(_context.SheetMusics, "Id", "Name");
            return View();
        }

        // POST: RehearsalSheetMusics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rehearsalSheetMusic"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RehearsalId,SheetMusicId,Id")] RehearsalSheetMusic rehearsalSheetMusic)
        {
            if (ModelState.IsValid)
            {
                rehearsalSheetMusic.Id = Guid.NewGuid();
                _context.Add(rehearsalSheetMusic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RehearsalId"] = new SelectList(_context.Rehearsals, "Id", "Location", rehearsalSheetMusic.RehearsalId);
            ViewData["SheetMusicId"] = new SelectList(_context.SheetMusics, "Id", "Name", rehearsalSheetMusic.SheetMusicId);
            return View(rehearsalSheetMusic);
        }

        // GET: RehearsalSheetMusics/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rehearsalSheetMusic = await _context.RehearsalSheetMusics.FindAsync(id);
            if (rehearsalSheetMusic == null)
            {
                return NotFound();
            }
            ViewData["RehearsalId"] = new SelectList(_context.Rehearsals, "Id", "Location", rehearsalSheetMusic.RehearsalId);
            ViewData["SheetMusicId"] = new SelectList(_context.SheetMusics, "Id", "Name", rehearsalSheetMusic.SheetMusicId);
            return View(rehearsalSheetMusic);
        }

        // POST: RehearsalSheetMusics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rehearsalSheetMusic"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RehearsalId,SheetMusicId,Id")] RehearsalSheetMusic rehearsalSheetMusic)
        {
            if (id != rehearsalSheetMusic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rehearsalSheetMusic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RehearsalSheetMusicExists(rehearsalSheetMusic.Id))
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
            ViewData["RehearsalId"] = new SelectList(_context.Rehearsals, "Id", "Location", rehearsalSheetMusic.RehearsalId);
            ViewData["SheetMusicId"] = new SelectList(_context.SheetMusics, "Id", "Name", rehearsalSheetMusic.SheetMusicId);
            return View(rehearsalSheetMusic);
        }

        // GET: RehearsalSheetMusics/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rehearsalSheetMusic = await _context.RehearsalSheetMusics
                .Include(r => r.Rehearsal)
                .Include(r => r.SheetMusic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rehearsalSheetMusic == null)
            {
                return NotFound();
            }

            return View(rehearsalSheetMusic);
        }

        // POST: RehearsalSheetMusics/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var rehearsalSheetMusic = await _context.RehearsalSheetMusics.FindAsync(id);
            _context.RehearsalSheetMusics.Remove(rehearsalSheetMusic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RehearsalSheetMusicExists(Guid id)
        {
            return _context.RehearsalSheetMusics.Any(e => e.Id == id);
        }
    }
}
