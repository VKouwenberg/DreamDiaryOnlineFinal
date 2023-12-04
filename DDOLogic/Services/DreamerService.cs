using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDDO.ModelsDTO;
using DataAccessDDO.Repositories;
using DDOLogic.Models;
using LogicDDO.Models;

namespace LogicDDO.Services;

public class DreamerService
{
	private readonly DreamerRepo _dreamerRepo;

	public DreamerService(DreamerRepo dreamerRepo)
	{
		this._dreamerRepo = dreamerRepo;
	}

	private Dreamer ConvertDTOToDreamer(DreamerDTO dto)
	{
		//Dreamer dreamer = new Dreamer(dto.DreamerId, dto.DreamerName);
		Dreamer dreamer = new Dreamer
		{
			DreamerId = dto.DreamerId,
			DreamerName = dto.DreamerName
		};

		return dreamer;
	}

	public List<Dreamer> ConvertDreamerDTOsToDreamers()
	{
		List<DreamerDTO> dreamerDTOs = _dreamerRepo.GetAllDreamers();

		List<Dreamer> dreamers = new List<Dreamer>();

		foreach (DreamerDTO dto in dreamerDTOs)
		{
			Dreamer dreamer = ConvertDTOToDreamer(dto);
			dreamers.Add(dreamer);
		}

		return dreamers;
	}
}
