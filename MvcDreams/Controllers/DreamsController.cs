using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogicDDO.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcDreams.Data;
using MvcDreams.Models;
using LogicDDO.Models;
using DataAccessDDO.ModelsDTO;

namespace MvcDreams.Controllers
{
    public class DreamsController : Controller
    {

        ///old context

        /*private readonly MvcDreamsContext _context;

        public DreamsController(MvcDreamsContext context)
        {
            _context = context;
        }*/

        ///Not DbSet context, so wrong? Maybe right after all?
        /////gotta be a DbSet<>
        private readonly DreamService _dreamService;

        public DreamsController(DreamService dreamService)
        {
            _dreamService = dreamService;
        }


        /*private readonly DataAccessDDOContext _context;

        public DreamsController(DataAccessDDOContext context)
        {
            _context = context;
        }*/


        // GET: Dreams
        /*public async Task<IActionResult> Index()
        {
              return _context.Dream != null ? 
                          View(await _context.Dream.ToListAsync()) :
                          Problem("Entity set 'MvcDreamsContext.Dream'  is null.");
        }*/



        private List<Models.Dream> MapToViewModels(List<LogicDDO.Models.Dream> dreamEntities)
        {
            var dreamViewModels = new List<Models.Dream>();

            foreach (var dreamEntity in dreamEntities)
            {
                var dreamViewModel = new Models.Dream
                {
                    Id = dreamEntity.DreamId,
                    Name = dreamEntity.DreamName,
                    ReadableBy = dreamEntity.ReadableBy,
                    DreamText = dreamEntity.DreamText
                    
                    //mapping
                };

                dreamViewModels.Add(dreamViewModel);
            }

            return dreamViewModels;
        }

        [HttpPost] 
        public string Index(string searchString, bool notUsed) //doubt
        {
            var dreamEntities = _dreamService.ConvertDreamDTOsToDreams(); //pulls logicmodels to here
            var dreamViewModels = MapToViewModels(dreamEntities);

            return "From [HttpPost]Index: filter on " + searchString;
        }

        public async Task<IActionResult> Index(string dreamTag, string searchString)
        {
            if (_context.Dreams == null)
            {
                return Problem("Entity set 'MvcDreamsContext.Dream'  is null.");
            }

            // Use LINQ to get list of genres.
            IQueryable<string> tagQuery = from m in _context.Dream
                                            orderby m.Tag
                                            select m.Tag;
            var dreams = from m in _context.Dream
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                dreams = dreams.Where(s => s.Name!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(dreamTag))
            {
                dreams = dreams.Where(x => x.Tag == dreamTag);
            }

            var dreamTagVM = new DreamTagViewModel
            {
                Tags = new SelectList(await tagQuery.Distinct().ToListAsync()),
                Dreams = await dreams.ToListAsync()
            };

            return View(dreamTagVM);
        }


        // GET: Dreams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Dream == null)
            {
                return NotFound();
            }

            var dream = await _context.Dream
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dream == null)
            {
                return NotFound();
            }

            return View(dream);
        }

        // GET: Dreams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dreams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,UploadDate,ReadableBy")] Dream dream)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dream);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dream);
        }

        // GET: Dreams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Dream == null)
            {
                return NotFound();
            }

            var dream = await _context.Dream.FindAsync(id);
            if (dream == null)
            {
                return NotFound();
            }
            return View(dream);
        }

        // POST: Dreams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UploadDate,ReadableBy")] Dream dream)
        {
            if (id != dream.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dream);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DreamExists(dream.Id))
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
            return View(dream);
        }

        // GET: Dreams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Dream == null)
            {
                return NotFound();
            }

            var dream = await _context.Dream
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dream == null)
            {
                return NotFound();
            }

            return View(dream);
        }

        // POST: Dreams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Dream == null)
            {
                return Problem("Entity set 'MvcDreamsContext.Dream'  is null.");
            }
            var dream = await _context.Dream.FindAsync(id);
            if (dream != null)
            {
                _context.Dream.Remove(dream);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DreamExists(int id)
        {
          return (_context.Dream?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
