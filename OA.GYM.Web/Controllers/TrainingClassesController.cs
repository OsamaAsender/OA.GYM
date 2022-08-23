using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OA.GYM.Entities;
using OA.GYM.Web.Data;
using OA.GYM.Web.Models.TrainingClasses;

namespace OA.GYM.Web.Controllers
{
    public class TrainingClassesController : Controller
    {
        #region Data const
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public TrainingClassesController(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion



        #region Actions
        public async Task<IActionResult> Index()
        {
            var trainingclass =
                             
                await _context.TrainingClasses

                .Include(t => t.ClassType)
                .Include(t => t.Coach)                     
                .ToListAsync();

            var trainingclassVMs = _mapper.Map<List<TrainingClass>>(trainingclass);
            return View(trainingclassVMs);
        }

    
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
            var trainingclassVMs = _mapper.Map<TrainingClassesViewModel>(trainingClass);
            return View(trainingclassVMs);
        }

        public IActionResult Create()
        {
            ViewData["ClassTypeId"] = new SelectList(_context.ClassTypes, "Id", "Name");
            ViewData["CoachId"] = new SelectList(_context.Coaches, "Id", "FirstName");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrainingClass trainingClass)
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

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,TrainingClass trainingClass)
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
        #endregion Actions

        #region Private Functions
        private bool TrainingClassExists(int id)
        {
          return (_context.TrainingClasses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        #endregion
    }
}
