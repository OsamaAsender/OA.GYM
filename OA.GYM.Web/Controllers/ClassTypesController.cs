﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OA.GYM.Entities;
using OA.GYM.Web.Data;
using OA.GYM.Web.Models.ClassTypes;

namespace OA.GYM.Web.Controllers
{
    public class ClassTypesController : Controller
    {
        #region Data const
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ClassTypesController(ApplicationDbContext context ,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region Actions
        // GET: ClassTypes
        public async Task<IActionResult> Index()
        {
            var classTypes = await _context.ClassTypes.ToListAsync();
            var classTypesVMs = _mapper.Map<List<ClassType>, List<ClassTypesViewModel>>(classTypes);
            return View(classTypesVMs);
                          
                          
        }

        // GET: ClassTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClassTypes == null)
            {
                return NotFound();
            }

            var classType = await _context
                                       .ClassTypes
                                       .FirstOrDefaultAsync(m => m.Id == id);

            if (classType == null)
            {
                return NotFound();
            }
            var classTypesVMs = _mapper.Map<ClassType, ClassTypesViewModel>(classType); 

            return View(classTypesVMs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClassTypesViewModel classTypeVM)
        {
            if (ModelState.IsValid)
            {
                var classtype = _mapper.Map<ClassTypesViewModel, ClassType>(classTypeVM);
                _context.Add(classtype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classTypeVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClassTypes == null)
            {
                return NotFound();
            }

            var classtype = await _context.ClassTypes.FindAsync(id);
            if (classtype == null)
            {
                return NotFound();
            }
            var classTypeVM = _mapper.Map<ClassType, ClassTypesViewModel>(classtype);
            return View(classTypeVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClassTypesViewModel classTypeVM)
        {
            if (id != classTypeVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var classType = _mapper.Map<ClassTypesViewModel, ClassType>(classTypeVM);
                    _context.Update(classType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassTypeExists(classTypeVM.Id))
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
            return View(classTypeVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClassTypes == null)
            {
                return NotFound();
            }

            var classType = await _context.ClassTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classType == null)
            {
                return NotFound();
            }
            var classTypeVM = _mapper.Map<ClassType, ClassTypesViewModel>(classType);
            return View(classTypeVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClassTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ClassTypeList'  is null.");
            }
            var classType = await _context.ClassTypes.FindAsync(id);
            if (classType != null)
            {
                _context.ClassTypes.Remove(classType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Private Methods
        private bool ClassTypeExists(int id)
        {
          return (_context.ClassTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        #endregion
    }
}
