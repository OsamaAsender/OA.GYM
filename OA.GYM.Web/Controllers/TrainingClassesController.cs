using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OA.GYM.Entities;
using OA.GYM.Web.Data;

namespace OA.GYM.Web.Controllers
{
    public class TrainingClassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TrainingClasses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TrainingClasses.Include(t => t.ClassType).Include(t => t.Coach);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TrainingClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TrainingClasses == null)
            {
                return NotFound();
            }

            var trainingClass = await _context.TrainingClasses
                .Include(t => t.ClassType)
                .Include(t => t.Coach)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingClass == null)
            {
                return NotFound();
            }

            return View(trainingClass);
        }

        // GET: TrainingClasses/Create
        public IActionResult Create()
        {
            ViewData["ClassTypeId"] = new SelectList(_context.ClassTypes, "Id", "Name");
            ViewData["CoachId"] = new SelectList(_context.Coaches, "Id", "FirstName");
            return View();
        }

        // POST: TrainingClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,StartTime,ClassTypeId,CoachId")] TrainingClass trainingClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassTypeId"] = new SelectList(_context.ClassTypes, "Id", "Name", trainingClass.ClassTypeId);
            ViewData["CoachId"] = new SelectList(_context.Coaches, "Id", "FirstName", trainingClass.CoachId);
            return View(trainingClass);
        }

        // GET: TrainingClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TrainingClasses == null)
            {
                return NotFound();
            }

            var trainingClass = await _context.TrainingClasses.FindAsync(id);
            if (trainingClass == null)
            {
                return NotFound();
            }
            ViewData["ClassTypeId"] = new SelectList(_context.ClassTypes, "Id", "Name", trainingClass.ClassTypeId);
            ViewData["CoachId"] = new SelectList(_context.Coaches, "Id", "FirstName", trainingClass.CoachId);
            return View(trainingClass);
        }

        // POST: TrainingClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,StartTime,ClassTypeId,CoachId")] TrainingClass trainingClass)
        {
            if (id != trainingClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingClassExists(trainingClass.Id))
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
            ViewData["ClassTypeId"] = new SelectList(_context.ClassTypes, "Id", "Name", trainingClass.ClassTypeId);
            ViewData["CoachId"] = new SelectList(_context.Coaches, "Id", "FirstName", trainingClass.CoachId);
            return View(trainingClass);
        }

        // GET: TrainingClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TrainingClasses == null)
            {
                return NotFound();
            }

            var trainingClass = await _context.TrainingClasses
                .Include(t => t.ClassType)
                .Include(t => t.Coach)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingClass == null)
            {
                return NotFound();
            }

            return View(trainingClass);
        }

        // POST: TrainingClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TrainingClasses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TrainingClasses'  is null.");
            }
            var trainingClass = await _context.TrainingClasses.FindAsync(id);
            if (trainingClass != null)
            {
                _context.TrainingClasses.Remove(trainingClass);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingClassExists(int id)
        {
          return (_context.TrainingClasses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
