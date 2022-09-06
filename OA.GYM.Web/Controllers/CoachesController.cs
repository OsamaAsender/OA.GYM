using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OA.GYM.Entities;
using OA.GYM.Web.Data;
using OA.GYM.Web.Models.Coaches;

namespace OA.GYM.Web.Controllers
{
    public class CoachesController : Controller
    {
        #region Data Const
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CoachesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region Actions
        public async Task<IActionResult> Index()
        {
            var coaches = await _context
                                .Coaches
                                    .ToListAsync();
            var coachesVM = _mapper.Map<List<Coach>, List<CoachViewModel>>(coaches);
            return View(coachesVM);
        }

        // GET: Coaches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Coaches == null)
            {
                return NotFound();
            }

            var coach = await _context
                                     .Coaches
                                     .FirstOrDefaultAsync(m => m.Id == id);
            if (coach == null)
            {
                return NotFound();
            }

            var coachVM = _mapper.Map<Coach, CoachViewModel>(coach);
            return View(coachVM);
        }

        // GET: Coaches/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CoachViewModel coachVM)
        {
            if (ModelState.IsValid)
            {
                var coach = _mapper.Map<CoachViewModel, Coach>(coachVM);
                _context.Add(coach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coachVM);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Coaches == null)
            {
                return NotFound();
            }

            var coach = await _context.Coaches.FindAsync(id);
            if (coach == null)
            {
                return NotFound();
            }
            var coachVM = _mapper.Map<Coach, CoachViewModel>(coach);
            return View(coachVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CoachViewModel coachVM)
        {
            if (id != coachVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var coach = _mapper.Map<CoachViewModel, Coach>(coachVM);
                    _context.Update(coach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoachExists(coachVM.Id))
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

            return View(coachVM);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Coaches == null)
            {
                return NotFound();
            }

            var coach = await _context.Coaches
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coach == null)
            {
                return NotFound();
            }
            var coachVM = _mapper.Map<Coach, CoachViewModel>(coach);
            return View(coachVM);
        }

        // POST: Coaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Coaches == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CoachesList'  is null.");
            }
            var coach = await _context.Coaches.FindAsync(id);
            if (coach != null)
            {
                _context.Coaches.Remove(coach);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Private Functions
        private bool CoachExists(int id)
        {
            return (_context.Coaches?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        #endregion
    }
}
