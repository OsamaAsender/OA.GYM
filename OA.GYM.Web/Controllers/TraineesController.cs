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
using OA.GYM.Web.Models.Trainees;

namespace OA.GYM.Web.Controllers
{
    public class TraineesController : Controller
    {
        #region Data const
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public TraineesController(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region
        // GET: Trainees
        public async Task<IActionResult> Index()
        {
           var trainees =  await _context.Trainees.ToListAsync();
            var traineeVMs = _mapper.Map<List<Trainee>, List<TraineesViewModel>>(trainees); 
                         return View(traineeVMs);
        }

        // GET: Trainees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Trainees == null)
            {
                return NotFound();
            }

            var trainee = await _context.Trainees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainee == null)
            {
                return NotFound();
            }
            var traineesVMs = _mapper.Map<Trainee, TraineesViewModel>(trainee);
            return View(traineesVMs);
        }

        // GET: Trainees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trainees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TraineesViewModel traineeVMs)
        {
            if (ModelState.IsValid)
            {
                var trainee = _mapper.Map<TraineesViewModel, Trainee>(traineeVMs);
                _context.Add(trainee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(traineeVMs);
        }

        // GET: Trainees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Trainees == null)
            {
                return NotFound();
            }

            var trainee = await _context.Trainees.FindAsync(id);
            if (trainee == null)
            {
                return NotFound();
            }
            var traineeVMs = _mapper.Map<Trainee, TraineesViewModel>(trainee);
            return View(traineeVMs);
        }

        // POST: Trainees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TraineesViewModel traineeVMs)
        {
            if (id != traineeVMs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var trainee = _mapper.Map<TraineesViewModel, Trainee>(traineeVMs);
                    _context.Update(trainee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TraineeExists(traineeVMs.Id))
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
            return View(traineeVMs);
        }

        // GET: Trainees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Trainees == null)
            {
                return NotFound();
            }

            var trainee = await _context.Trainees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainee == null)
            {
                return NotFound();
            }
            var traineeVMs = _mapper.Map<Trainee, TraineesViewModel>(trainee);
            return View(traineeVMs);
        }

        // POST: Trainees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Trainees == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Trainees'  is null.");
            }
            var trainee = await _context.Trainees.FindAsync(id);
            if (trainee != null)
            {
                _context.Trainees.Remove(trainee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region private Methods
        private bool TraineeExists(int id)
        {
          return (_context.Trainees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        #endregion
    }
}
