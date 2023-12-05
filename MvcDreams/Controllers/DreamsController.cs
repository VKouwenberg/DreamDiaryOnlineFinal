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
using MvcDreams.Models; // Ensure this namespace is included
using MvcDreams; // Include your DreamService namespace

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

	[HttpGet]
	public async Task<IActionResult> Index(string searchString)
	{
		var dreamViewModels = await GetFilteredDreamViewModelsAsync(searchString);

		return View(dreamViewModels);
	}

	private async Task<List<DreamViewModel>> GetFilteredDreamViewModelsAsync(string searchString)
	{
		var dreamEntities = await _dreamService.ConvertDreamDTOsToDreamsAsync();
		var dreamViewModels = MapToViewModels(dreamEntities);

		if (!string.IsNullOrEmpty(searchString))
		{
			dreamViewModels = dreamViewModels
				.Where(d => d.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
				.ToList();
		}

		return dreamViewModels;
	}

	private List<DreamViewModel> MapToViewModels(List<Dream> dreamEntities)
	{
		var dreamViewModels = new List<DreamViewModel>();

		foreach (var dreamEntity in dreamEntities)
		{
			var dreamViewModel = new DreamViewModel
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
}

