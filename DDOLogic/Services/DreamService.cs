﻿using LogicDDO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDDO.ModelsDTO;
using DataAccessDDO.Repositories;
using Org.BouncyCastle.Bcpg; //what even is this

namespace LogicDDO.Services;

public class DreamService
{
	private readonly List<Dream> _dreamList;
	private readonly DreamRepo _dreamRepo;

	public DreamService(DreamRepo dreamRepo)
	{
		_dreamList = new List<Dream>();
		_dreamRepo = dreamRepo;
		SyncWithDatabase();
	}

	private void SyncWithDatabase()
	{
		_dreamList.Clear();
		List<Dream> dreams = ConvertDreamDTOsToDreams();
		_dreamList.AddRange(dreams);
	}

	//This creates logicmodels that are intended to be sent to the view as viewmodels
	//These only contain the data the view needs.
	// /It happens to be the same, but that is not always the case.
	private List<Dream> MapToViewModels(List<Dream> logicDreams)
	{
		List<Dream> dreamViewModels = new List<Dream>();

		foreach (var logicDream in logicDreams)
		{
			Dream dreamViewModel = new Dream
			{
				DreamId = logicDream.DreamId,
				DreamName = logicDream.DreamName,
				DreamText = logicDream.DreamText,
				ReadableBy = logicDream.ReadableBy,
				DreamerId = logicDream.DreamerId
			};

			dreamViewModels.Add(dreamViewModel);
		}

		return dreamViewModels;
	}

	public List<Dream> GetDreamViewModels()
	{
		return MapToViewModels(_dreamList);
	}

	private Dream ConvertDTOToDream(DreamDTO dto)
	{
		//Dreamer dreamer = new Dreamer(dto.DreamerId, dto.DreamerName);
		Dream dream = new Dream
		{
			DreamerId = dto.DreamerId,
			DreamName = dto.DreamName,
			DreamText = dto.DreamText,
			ReadableBy = dto.ReadableBy,
			DreamId = dto.DreamId
		};

		return dream;
	}


	//Maps DTOs to logic models
	public List<Dream> ConvertDreamDTOsToDreams()
	{
		List<DreamDTO> dreamDTOs = _dreamRepo.GetAllDreams();

		List<Dream> dreams = new List<Dream>();

		foreach (DreamDTO dto in dreamDTOs)
		{
			Dream dream = ConvertDTOToDream(dto);
			dreams.Add(dream);
		}

		return dreams;
	}

	//maps logic model to DTO
	private DreamDTO MapToDTO(Dream dream)
	{
		return new DataAccessDDO.ModelsDTO.DreamDTO
		{
			DreamId = dream.DreamId,
			DreamName = dream.DreamName,
			DreamText = dream.DreamText,
			ReadableBy = dream.ReadableBy,
			DreamerId = dream.DreamerId
		};
	}

	//CRUD
	public void CreateDream(Dream dream)
	{
		var dreamDTO = MapToDTO(dream);
		_dreamRepo.CreateDream(dreamDTO);
	}

	public List<Dream> GetDreams()
	{
		var dreamDTOs = ConvertDreamDTOsToDreams();
		return dreamDTOs;
	}

	public void UpdateDream(Dream dream)
	{
		var dreamDTO = MapToDTO(dream);
		_dreamRepo.UpdateDream(dreamDTO);
	}

	public void DeleteDream(int dreamId)
	{
		_dreamRepo.DeleteDream(dreamId);
	}
}
