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

    public class ConcertSheetMusicsController : Controller
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ConcertSheetMusicsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ConcertSheetMusics
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ConcertSheetMusics.Include(c => c.Concert).Include(c => c.SheetMusic);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ConcertSheetMusics/Details/5
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

            var concertSheetMusic = await _context.ConcertSheetMusics
                .Include(c => c.Concert)
                .Include(c => c.SheetMusic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concertSheetMusic == null)
            {
                return NotFound();
            }

            return View(concertSheetMusic);
        }

        // GET: ConcertSheetMusics/Create
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            ViewData["ConcertId"] = new SelectList(_context.Concerts, "Id", "Id");
            ViewData["SheetMusicId"] = new SelectList(_context.SheetMusics, "Id", "Name");
            return View();
        }

        // POST: ConcertSheetMusics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="concertSheetMusic"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConcertId,SheetMusicId,Id")] ConcertSheetMusic concertSheetMusic)
        {
            if (ModelState.IsValid)
            {
                concertSheetMusic.Id = Guid.NewGuid();
                _context.Add(concertSheetMusic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConcertId"] = new SelectList(_context.Concerts, "Id", "Id", concertSheetMusic.ConcertId);
            ViewData["SheetMusicId"] = new SelectList(_context.SheetMusics, "Id", "Name", concertSheetMusic.SheetMusicId);
            return View(concertSheetMusic);
        }

        // GET: ConcertSheetMusics/Edit/5
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

            var concertSheetMusic = await _context.ConcertSheetMusics.FindAsync(id);
            if (concertSheetMusic == null)
            {
                return NotFound();
            }
            ViewData["ConcertId"] = new SelectList(_context.Concerts, "Id", "Id", concertSheetMusic.ConcertId);
            ViewData["SheetMusicId"] = new SelectList(_context.SheetMusics, "Id", "Name", concertSheetMusic.SheetMusicId);
            return View(concertSheetMusic);
        }

        // POST: ConcertSheetMusics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="concertSheetMusic"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ConcertId,SheetMusicId,Id")] ConcertSheetMusic concertSheetMusic)
        {
            if (id != concertSheetMusic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(concertSheetMusic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConcertSheetMusicExists(concertSheetMusic.Id))
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
            ViewData["ConcertId"] = new SelectList(_context.Concerts, "Id", "Id", concertSheetMusic.ConcertId);
            ViewData["SheetMusicId"] = new SelectList(_context.SheetMusics, "Id", "Name", concertSheetMusic.SheetMusicId);
            return View(concertSheetMusic);
        }

        // GET: ConcertSheetMusics/Delete/5
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

            var concertSheetMusic = await _context.ConcertSheetMusics
                .Include(c => c.Concert)
                .Include(c => c.SheetMusic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concertSheetMusic == null)
            {
                return NotFound();
            }

            return View(concertSheetMusic);
        }

        // POST: ConcertSheetMusics/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var concertSheetMusic = await _context.ConcertSheetMusics.FindAsync(id);
            _context.ConcertSheetMusics.Remove(concertSheetMusic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConcertSheetMusicExists(Guid id)
        {
            return _context.ConcertSheetMusics.Any(e => e.Id == id);
        }
    }
}
