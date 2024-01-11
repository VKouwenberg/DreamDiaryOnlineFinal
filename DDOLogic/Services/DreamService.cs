using LogicDDO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDDO.ModelsDTO;
using DataAccessDDO.Repositories;
using LogicDDO.Services.LogicInterfaces;
using DataAccessDDO.Repositories.DataAccessInterfaces;

namespace LogicDDO.Services;

public class DreamService : LogicInterfaces.IDreamService
{
	private readonly IDreamRepository _dreamRepo;
	private readonly ITagService _tagService;

	public DreamService(IDreamRepository dreamRepo, ITagService tagService)
	{
		_dreamRepo = dreamRepo;
		_tagService = tagService;
	}

	public Dream GetDreamById(int id)
	{
		Console.WriteLine("ID SEND TO RETRIEVE " + id);

		DreamDTO dto = _dreamRepo.GetDreamById(id);

		Console.WriteLine("DREAM RETRIEVED FROM DATABASE BY ID " + dto.DreamId);
		Console.WriteLine(dto.DreamName);
		if (dto.Tags != null && dto.Tags.Any()) 
		{
			foreach (TagDTO tag in dto.Tags)
			{
				Console.WriteLine(tag.TagName);
			}
		}
		Console.WriteLine("READY TO MAP DREAMDTO TO DREAM LOGIC MODEL");
		Dream dream = MapDreamDTOToDream(dto);
		return dream;
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

	public void DeleteDream(int id)
	{
		_dreamRepo.DeleteDream(id);
	}

	public void UpdateDream(Dream dream)
	{
		DreamDTO dto = MapToDTO(dream);
		_dreamRepo.UpdateDream(dto);
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
			DreamId = dto.DreamId,
			Tags = new List<Tag>()
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
