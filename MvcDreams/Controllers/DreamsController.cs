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

namespace MvcDreams.Controllers;

public class DreamsController : Controller
{
	private readonly DreamService _dreamService;

	public DreamsController(DreamService dreamService)
	{
		_dreamService = dreamService;
	}

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
			};

			dreamViewModels.Add(dreamViewModel);
		}

		return dreamViewModels;
	}

	//converts DTOs to logic models
	//converts logic models to view models
	private List<Models.Dream> GetDreamViewModels()
	{
		var dreamEntities = _dreamService.ConvertDreamDTOsToDreams();
		var dreamViewModels = MapToViewModels(dreamEntities);
		return dreamViewModels;
	}

	[HttpPost]
	public string Index(string searchString, bool notUsed) //doubt
	{
		return "From [HttpPost]Index: filter on " + searchString;
	}

	public async Task<IActionResult> Index(string searchString)
	{
		var dreamViewModels = GetDreamViewModels();

		if (dreamViewModels == null)
		{
			return Problem("Entity set 'MvcDreamsContext.Dream'  is null.");
		}

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
		//pulls logicmodels to here
		var dreamViewModels = GetDreamViewModels();

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
	public async Task<IActionResult> Create([Bind("Id,Name,UploadDate,ReadableBy")] Models.Dream dream)
	{
		//pulls logicmodels to here
		var dreamViewModels = GetDreamViewModels();

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
		//pulls logicmodels to here
		var dreamViewModels = GetDreamViewModels();

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
	public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UploadDate,ReadableBy")] Models.Dream dream)
	{
		//pulls logicmodels to here
		var dreamViewModels = GetDreamViewModels();

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
		//pulls logicmodels to here
		var dreamViewModels = GetDreamViewModels();

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
		//pulls logicmodels to here
		var dreamViewModels = GetDreamViewModels();

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

