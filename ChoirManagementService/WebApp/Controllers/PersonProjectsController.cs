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

    public class PersonProjectsController : Controller
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public PersonProjectsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PersonProjects
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.PersonProjects.Include(p => p.AppUser).Include(p => p.Project).Include(p => p.VoiceGroup);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PersonProjects/Details/5
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

            var personProject = await _context.PersonProjects
                .Include(p => p.AppUser)
                .Include(p => p.Project)
                .Include(p => p.VoiceGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personProject == null)
            {
                return NotFound();
            }

            return View(personProject);
        }

        // GET: PersonProjects/Create
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name");
            ViewData["VoiceGroupId"] = new SelectList(_context.VoiceGroups, "Id", "Name");
            return View();
        }

        // POST: PersonProjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="personProject"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Status,Comment,VoiceGroupId,ProjectId,AppUserId,Id")] PersonProject personProject)
        {
            if (ModelState.IsValid)
            {
                personProject.Id = Guid.NewGuid();
                _context.Add(personProject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", personProject.AppUserId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", personProject.ProjectId);
            ViewData["VoiceGroupId"] = new SelectList(_context.VoiceGroups, "Id", "Name", personProject.VoiceGroupId);
            return View(personProject);
        }

        // GET: PersonProjects/Edit/5
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

            var personProject = await _context.PersonProjects.FindAsync(id);
            if (personProject == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", personProject.AppUserId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", personProject.ProjectId);
            ViewData["VoiceGroupId"] = new SelectList(_context.VoiceGroups, "Id", "Name", personProject.VoiceGroupId);
            return View(personProject);
        }

        // POST: PersonProjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personProject"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Status,Comment,VoiceGroupId,ProjectId,AppUserId,Id")] PersonProject personProject)
        {
            if (id != personProject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonProjectExists(personProject.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", personProject.AppUserId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", personProject.ProjectId);
            ViewData["VoiceGroupId"] = new SelectList(_context.VoiceGroups, "Id", "Name", personProject.VoiceGroupId);
            return View(personProject);
        }

        // GET: PersonProjects/Delete/5
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

            var personProject = await _context.PersonProjects
                .Include(p => p.AppUser)
                .Include(p => p.Project)
                .Include(p => p.VoiceGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personProject == null)
            {
                return NotFound();
            }

            return View(personProject);
        }

        // POST: PersonProjects/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var personProject = await _context.PersonProjects.FindAsync(id);
            _context.PersonProjects.Remove(personProject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonProjectExists(Guid id)
        {
            return _context.PersonProjects.Any(e => e.Id == id);
        }
    }
}
