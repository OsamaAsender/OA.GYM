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
        public TrainingClassesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion



        #region Actions
        public async Task<IActionResult> Index()
        {
            var trainingClasses =

                await _context.TrainingClasses

                .Include(t => t.ClassType)
                .Include(t => t.Coach)
                .ToListAsync();

            var trainingclassVMs = _mapper.Map<List<TrainingClassListViewModel>>(trainingClasses);
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
                                 .Include(t => t.Trainees)
                                 .FirstOrDefaultAsync(m => m.Id == id);


            if (trainingClass == null)
            {
                return NotFound();
            }
            var trainingClassVM = _mapper.Map<TrainingClassDetailViewModel>(trainingClass);
            return View(trainingClassVM);
        }

        public IActionResult Create()
        {
            var trainingclassVM = new TrainingClassesViewModel();


            trainingclassVM.ClassTypeList = new SelectList(_context.ClassTypes, "Id", "Name");
            trainingclassVM.CoachesList = new SelectList(_context.Coaches, "Id", "FirstName");
            trainingclassVM.TraineesList = new MultiSelectList(_context.Trainees, "Id", "FullName");


            return View(trainingclassVM);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrainingClassesViewModel trainingClassVM)
        {
            if (ModelState.IsValid)
            {
                var trainingClass = _mapper.Map<TrainingClass>(trainingClassVM);

                var trainees = await _context                                                                                              //Calling the context of trainees from the database then assigning the ViewModel TraineeIds to the Entity TraineeIds and putting them into a list
                                                                                                                                          
                    .Trainees                                                                                                             
                    .Where(Trainee => trainingClassVM.TraineeIds.Contains(Trainee.Id))                                                     // brings trainees from database and assigns their Ids to the "ViewModel" trainee Ids
                    .ToListAsync();                                                                                                       
                                                                                                                                          
                trainingClass.Trainees.AddRange(trainees);                                                                                 //Add.Range add trainingClass.Trainees as a list to the var trainees

                _context.Add(trainingClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            trainingClassVM.ClassTypeList = new SelectList(_context.ClassTypes, "Id", "Name", trainingClassVM.ClassTypeId);
            trainingClassVM.CoachesList = new SelectList(_context.Coaches, "Id", "FirstName", trainingClassVM.CoachId); 
            trainingClassVM.TraineesList = new MultiSelectList(_context.Trainees, "Id", "FullName", trainingClassVM.TraineeIds);


            return View(trainingClassVM);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TrainingClasses == null)
            {
                return NotFound();
            }

            var trainingClass = await _context.TrainingClasses

                               .Include(t => t.ClassType)
                               .Include(t => t.Coach)
                               .Include(t => t.Trainees)
                               .FirstOrDefaultAsync(m => m.Id == id);

            if (trainingClass == null)
            {
                return NotFound();
            }



            var trainingClassVM = _mapper.Map<TrainingClassesViewModel>(trainingClass);


            trainingClassVM.ClassTypeList = new SelectList(_context.ClassTypes, "Id", "Name", trainingClass.ClassTypeId);


            trainingClassVM.CoachesList = new SelectList(_context.Coaches, "Id", "FirstName", trainingClass.CoachId);


            trainingClassVM.TraineeIds = trainingClass.Trainees.Select(t => t.Id).ToList();   //this line brings the selected TraineeIds in Create  from the database as a list and says the TraineeIds in the viewmodel == TraineeIds in the database
            trainingClassVM.TraineesList = new MultiSelectList(_context.Trainees, "Id", "FullName", trainingClassVM.TraineeIds);




            return View(trainingClassVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TrainingClassesViewModel trainingClassVM)
        {
            if (id != trainingClassVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var trainingClass = _mapper.Map<TrainingClass>(trainingClassVM);
                try
                {
                    _context.Update(trainingClass);
                    await _context.SaveChangesAsync();
                    await AddTraineesToTrainingClass(trainingClassVM, trainingClass.Id);
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
            trainingClassVM.ClassTypeList = new SelectList(_context.ClassTypes, "Id", "Name", trainingClassVM.ClassTypeId);
            trainingClassVM.CoachesList = new SelectList(_context.Coaches, "Id", "FirstName", trainingClassVM.CoachId);
            trainingClassVM.TraineesList = new MultiSelectList(_context.Trainees, "Id", "FullName", trainingClassVM.TraineesList);
            return View(trainingClassVM);
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

        private async Task AddTraineesToTrainingClass(TrainingClassesViewModel trainingClassVM, int trainingClassId)
        {
            var trainingClass = await _context
                                    .TrainingClasses
                                    .Include(tc => tc.Trainees)
                                    .Where(tc => trainingClassId == tc.Id)
                                    .SingleAsync();

            trainingClass.Trainees.Clear();

            var trainees = await _context
                .Trainees
                .Where(t => trainingClassVM.TraineeIds.Contains(t.Id))
                .ToListAsync();

            trainingClass.Trainees.AddRange(trainees);

            _context.Update(trainingClass);
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
