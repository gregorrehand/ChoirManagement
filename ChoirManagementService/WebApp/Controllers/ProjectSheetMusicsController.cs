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
    public class ProjectSheetMusicsController : Controller
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ProjectSheetMusicsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProjectSheetMusics
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ProjectSheetMusics.Include(p => p.Project).Include(p => p.SheetMusic);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ProjectSheetMusics/Details/5
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

            var projectSheetMusic = await _context.ProjectSheetMusics
                .Include(p => p.Project)
                .Include(p => p.SheetMusic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectSheetMusic == null)
            {
                return NotFound();
            }

            return View(projectSheetMusic);
        }

        // GET: ProjectSheetMusics/Create
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name");
            ViewData["SheetMusicId"] = new SelectList(_context.SheetMusics, "Id", "Name");
            return View();
        }

        // POST: ProjectSheetMusics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectSheetMusic"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectId,SheetMusicId,Id")] ProjectSheetMusic projectSheetMusic)
        {
            if (ModelState.IsValid)
            {
                projectSheetMusic.Id = Guid.NewGuid();
                _context.Add(projectSheetMusic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", projectSheetMusic.ProjectId);
            ViewData["SheetMusicId"] = new SelectList(_context.SheetMusics, "Id", "Name", projectSheetMusic.SheetMusicId);
            return View(projectSheetMusic);
        }

        // GET: ProjectSheetMusics/Edit/5
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

            var projectSheetMusic = await _context.ProjectSheetMusics.FindAsync(id);
            if (projectSheetMusic == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", projectSheetMusic.ProjectId);
            ViewData["SheetMusicId"] = new SelectList(_context.SheetMusics, "Id", "Name", projectSheetMusic.SheetMusicId);
            return View(projectSheetMusic);
        }

        // POST: ProjectSheetMusics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projectSheetMusic"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProjectId,SheetMusicId,Id")] ProjectSheetMusic projectSheetMusic)
        {
            if (id != projectSheetMusic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectSheetMusic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectSheetMusicExists(projectSheetMusic.Id))
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", projectSheetMusic.ProjectId);
            ViewData["SheetMusicId"] = new SelectList(_context.SheetMusics, "Id", "Name", projectSheetMusic.SheetMusicId);
            return View(projectSheetMusic);
        }

        // GET: ProjectSheetMusics/Delete/5
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

            var projectSheetMusic = await _context.ProjectSheetMusics
                .Include(p => p.Project)
                .Include(p => p.SheetMusic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectSheetMusic == null)
            {
                return NotFound();
            }

            return View(projectSheetMusic);
        }

        // POST: ProjectSheetMusics/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var projectSheetMusic = await _context.ProjectSheetMusics.FindAsync(id);
            _context.ProjectSheetMusics.Remove(projectSheetMusic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectSheetMusicExists(Guid id)
        {
            return _context.ProjectSheetMusics.Any(e => e.Id == id);
        }
    }
}
