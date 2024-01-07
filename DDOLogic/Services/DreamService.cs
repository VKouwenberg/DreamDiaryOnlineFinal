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
	private readonly DreamRepo _dreamRepo;
	private readonly TagService _tagService;

	public DreamService(DreamRepo dreamRepo, TagService tagService)
	{
		_dreamRepo = dreamRepo;
		_tagService = tagService;
	}

	//Calls private mapping method that converts DTOs to logic models
	//The mapping method is private because otherwise I'd have to link the DAL to the view
	//Now you can Get Dreams without having to pass on a list
	public List<Dream> GetDreams()
	{
		List<Dream> dreams = MapDreamDTOsToDreams(_dreamRepo.GetAllDreams());

        return dreams;
    }
	

	

	//converts a logic dream into a dream dto, which is then inserted into the database
	public void CreateDream(Dream dream)
	{
		DreamDTO dto = MapToDTO(dream);
		_dreamRepo.CreateDream(dto);
	}

	//maps logic model to DTO
	private DreamDTO MapToDTO(Dream dream)
	{
		DreamDTO dto = new DreamDTO()
		{
			DreamId = dream.DreamId,
			DreamName = dream.DreamName,
			DreamText = dream.DreamText,
			ReadableBy = dream.ReadableBy
		};

		if (dream.Tags != null && dream.Tags.Any())
		{
			dto.Tags = _tagService.MapTagsToTagDTOs(dream.Tags);
		}

		return dto;
	}

	//2 methods map DTOs to logic models
	private Dream MapDreamDTOToDream(DreamDTO dto)
	{
		Dream dream = new Dream
		{
			DreamName = dto.DreamName,
			DreamText = dto.DreamText,
			ReadableBy = dto.ReadableBy,
			DreamId = dto.DreamId
		};

		if (dto.Tags != null && dto.Tags.Any())
		{
			List<Tag> dtoTags = _tagService.MapTagDTOsToTags(dto.Tags);
			dream.Tags = dtoTags;

		}


		return dream;
	}

	private List<Dream> MapDreamDTOsToDreams(List<DreamDTO> dTOs)
	{
		List<Dream> dreams = new List<Dream>();

		foreach (DreamDTO dto in dTOs)
		{
			Dream dream = MapDreamDTOToDream(dto);
			dreams.Add(dream);
		}

		return dreams;
	}
}
