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

    public class PersonConcertsController : Controller
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public PersonConcertsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PersonConcerts
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.PersonConcerts.Include(p => p.AppUser).Include(p => p.Concert);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PersonConcerts/Details/5
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

            var personConcert = await _context.PersonConcerts
                .Include(p => p.AppUser)
                .Include(p => p.Concert)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personConcert == null)
            {
                return NotFound();
            }

            return View(personConcert);
        }

        // GET: PersonConcerts/Create
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["ConcertId"] = new SelectList(_context.Concerts, "Id", "Id");
            return View();
        }

        // POST: PersonConcerts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="personConcert"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Status,Comment,ConcertId,AppUserId,Id")] PersonConcert personConcert)
        {
            if (ModelState.IsValid)
            {
                personConcert.Id = Guid.NewGuid();
                _context.Add(personConcert);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", personConcert.AppUserId);
            ViewData["ConcertId"] = new SelectList(_context.Concerts, "Id", "Id", personConcert.ConcertId);
            return View(personConcert);
        }

        // GET: PersonConcerts/Edit/5
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

            var personConcert = await _context.PersonConcerts.FindAsync(id);
            if (personConcert == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", personConcert.AppUserId);
            ViewData["ConcertId"] = new SelectList(_context.Concerts, "Id", "Id", personConcert.ConcertId);
            return View(personConcert);
        }

        // POST: PersonConcerts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personConcert"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Status,Comment,ConcertId,AppUserId,Id")] PersonConcert personConcert)
        {
            if (id != personConcert.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personConcert);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonConcertExists(personConcert.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", personConcert.AppUserId);
            ViewData["ConcertId"] = new SelectList(_context.Concerts, "Id", "Id", personConcert.ConcertId);
            return View(personConcert);
        }

        // GET: PersonConcerts/Delete/5
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

            var personConcert = await _context.PersonConcerts
                .Include(p => p.AppUser)
                .Include(p => p.Concert)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personConcert == null)
            {
                return NotFound();
            }

            return View(personConcert);
        }

        // POST: PersonConcerts/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var personConcert = await _context.PersonConcerts.FindAsync(id);
            _context.PersonConcerts.Remove(personConcert);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonConcertExists(Guid id)
        {
            return _context.PersonConcerts.Any(e => e.Id == id);
        }
    }
}
