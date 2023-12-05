using LogicDDO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDDO.ModelsDTO;
using DataAccessDDO.Repositories;
using LogicDDO.Models;

namespace LogicDDO.Services;

public class DreamService
{
	private readonly DreamRepo _dreamRepo;

	public DreamService(DreamRepo dreamRepo)
	{
		this._dreamRepo = dreamRepo;
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
}
