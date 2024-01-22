using LogicDDO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicDDO.Services.DataAccessRepositoriesInterfaces;
using LogicDDO.Services.LogicServicesInterfaces;
using LogicDDO.ModelsDataAccessDTOs;

namespace LogicDDO.Services;

public class DreamService : IDreamService
{
	private readonly IDreamRepository _dreamRepository;
	private readonly ITagService _tagService;

	public DreamService(IDreamRepository dreamRepository, ITagService tagService)
	{
		_dreamRepository = dreamRepository;
		_tagService = tagService;
	}

	public List<Dream> GetAllDreams()
	{
		List<Dream> dreams = new List<Dream>();
		List<DreamDTOLogic> dreamDTOs = new List<DreamDTOLogic>();

		dreamDTOs = _dreamRepository.GetAllDreams();
		dreams = MapDreamDTOLogicsToDreams(dreamDTOs);

		return dreams;
	}

	public Dream GetDreamById(int id)
	{
		Dream dream = new Dream();
		DreamDTOLogic dreamDTO = new DreamDTOLogic();

		dreamDTO = _dreamRepository.GetDreamById(id);
		dream = MapDreamDTOLogicToDream(dreamDTO);

		return dream;
	}

	public void CreateDream(Dream dream)
	{
		DreamDTOLogic dreamDTO = MapDreamToDreamDTOLogic(dream);
		_dreamRepository.CreateDream(dreamDTO);
	}

	public void DeleteDream(int id)
	{
		_dreamRepository.DeleteDream(id);
	}

	public void UpdateDream(Dream dream)
	{
		DreamDTOLogic dreamDTO = MapDreamToDreamDTOLogic(dream);
		_dreamRepository.UpdateDream(dreamDTO);
	}

	//mapping logic
	public Dream MapDreamDTOLogicToDream(DreamDTOLogic dreamDTO)
	{
		Dream dream = new Dream
		{
			DreamId = dreamDTO.DreamId,
			DreamName = dreamDTO.DreamName,
			DreamText = dreamDTO.DreamText,
			ReadableBy = dreamDTO.ReadableBy,
			Tags = _tagService.MapTagDTOLogicsToTags(dreamDTO.Tags)
		};
		return dream;
	}
	public List<Dream> MapDreamDTOLogicsToDreams(List<DreamDTOLogic> dreamDTOs)
	{
		List<Dream> dreams = new List<Dream>();
		foreach (DreamDTOLogic dreamDTO in dreamDTOs)
		{
			Dream dream = MapDreamDTOLogicToDream(dreamDTO);
			dreams.Add(dream);
		}
		return dreams;
	}

	public DreamDTOLogic MapDreamToDreamDTOLogic(Dream dream)
	{
		DreamDTOLogic dreamDTO = new DreamDTOLogic
		{
			DreamId = dream.DreamId,
			DreamName = dream.DreamName,
			DreamText = dream.DreamText,
			ReadableBy = dream.ReadableBy,
			Tags = _tagService.MapTagsToTagDTOLogics(dream.Tags)
		};
		return dreamDTO;
	}

	public List<DreamDTOLogic> MapDreamsToDreamDTOLogics(List<Dream> dreams)
	{
		List<DreamDTOLogic> dreamDTOs = new List<DreamDTOLogic>();
		foreach (Dream dream in dreams)
		{
			DreamDTOLogic dreamDTO = MapDreamToDreamDTOLogic(dream);
			dreamDTOs.Add(dreamDTO);
		}
		return dreamDTOs;
	}
}
