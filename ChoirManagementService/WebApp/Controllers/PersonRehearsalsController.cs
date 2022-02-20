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
    public class PersonRehearsalsController : Controller
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public PersonRehearsalsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PersonRehearsals
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.PersonRehearsals.Include(p => p.AppUser).Include(p => p.Rehearsal);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PersonRehearsals/Details/5
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

            var personRehearsal = await _context.PersonRehearsals
                .Include(p => p.AppUser)
                .Include(p => p.Rehearsal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personRehearsal == null)
            {
                return NotFound();
            }

            return View(personRehearsal);
        }

        // GET: PersonRehearsals/Create
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["RehearsalId"] = new SelectList(_context.Rehearsals, "Id", "Location");
            return View();
        }

        // POST: PersonRehearsals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="personRehearsal"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Status,Comment,RehearsalId,AppUserId,Id")] PersonRehearsal personRehearsal)
        {
            if (ModelState.IsValid)
            {
                personRehearsal.Id = Guid.NewGuid();
                _context.Add(personRehearsal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", personRehearsal.AppUserId);
            ViewData["RehearsalId"] = new SelectList(_context.Rehearsals, "Id", "Location", personRehearsal.RehearsalId);
            return View(personRehearsal);
        }

        // GET: PersonRehearsals/Edit/5
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

            var personRehearsal = await _context.PersonRehearsals.FindAsync(id);
            if (personRehearsal == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", personRehearsal.AppUserId);
            ViewData["RehearsalId"] = new SelectList(_context.Rehearsals, "Id", "Location", personRehearsal.RehearsalId);
            return View(personRehearsal);
        }

        // POST: PersonRehearsals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personRehearsal"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Status,Comment,RehearsalId,AppUserId,Id")] PersonRehearsal personRehearsal)
        {
            if (id != personRehearsal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personRehearsal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonRehearsalExists(personRehearsal.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", personRehearsal.AppUserId);
            ViewData["RehearsalId"] = new SelectList(_context.Rehearsals, "Id", "Location", personRehearsal.RehearsalId);
            return View(personRehearsal);
        }

        // GET: PersonRehearsals/Delete/5
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

            var personRehearsal = await _context.PersonRehearsals
                .Include(p => p.AppUser)
                .Include(p => p.Rehearsal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personRehearsal == null)
            {
                return NotFound();
            }

            return View(personRehearsal);
        }

        // POST: PersonRehearsals/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var personRehearsal = await _context.PersonRehearsals.FindAsync(id);
            _context.PersonRehearsals.Remove(personRehearsal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonRehearsalExists(Guid id)
        {
            return _context.PersonRehearsals.Any(e => e.Id == id);
        }
    }
}
