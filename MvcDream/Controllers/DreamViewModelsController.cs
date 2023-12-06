using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcDream.Data;
using MvcDream.Models;

namespace MvcDream.Controllers
{
    public class DreamViewModelsController : Controller
    {
        private readonly MvcDreamContext _context;

        public DreamViewModelsController(MvcDreamContext context)
        {
            _context = context;
        }

        // GET: DreamViewModels
        public async Task<IActionResult> Index()
        {
              return _context.DreamViewModel != null ? 
                          View(await _context.DreamViewModel.ToListAsync()) :
                          Problem("Entity set 'MvcDreamContext.DreamViewModel'  is null.");
        }

        // GET: DreamViewModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DreamViewModel == null)
            {
                return NotFound();
            }

            var dreamViewModel = await _context.DreamViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dreamViewModel == null)
            {
                return NotFound();
            }

            return View(dreamViewModel);
        }

        // GET: DreamViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DreamViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DreamName,DreamText,ReadableBy,DreamerId")] DreamViewModel dreamViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dreamViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dreamViewModel);
        }

        // GET: DreamViewModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DreamViewModel == null)
            {
                return NotFound();
            }

            var dreamViewModel = await _context.DreamViewModel.FindAsync(id);
            if (dreamViewModel == null)
            {
                return NotFound();
            }
            return View(dreamViewModel);
        }

        // POST: DreamViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DreamName,DreamText,ReadableBy,DreamerId")] DreamViewModel dreamViewModel)
        {
            if (id != dreamViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dreamViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DreamViewModelExists(dreamViewModel.Id))
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
            return View(dreamViewModel);
        }

        // GET: DreamViewModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DreamViewModel == null)
            {
                return NotFound();
            }

            var dreamViewModel = await _context.DreamViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dreamViewModel == null)
            {
                return NotFound();
            }

            return View(dreamViewModel);
        }

        // POST: DreamViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DreamViewModel == null)
            {
                return Problem("Entity set 'MvcDreamContext.DreamViewModel'  is null.");
            }
            var dreamViewModel = await _context.DreamViewModel.FindAsync(id);
            if (dreamViewModel != null)
            {
                _context.DreamViewModel.Remove(dreamViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DreamViewModelExists(int id)
        {
          return (_context.DreamViewModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
