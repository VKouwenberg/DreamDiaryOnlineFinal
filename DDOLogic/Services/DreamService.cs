using LogicDDO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicDDO.Services.DataAccessRepositoriesInterfaces;

namespace LogicDDO.Services;

public class DreamService : IDreamRepository
{
	private readonly IDreamRepository _dreamRepo;
	private readonly TagService _tagService;

	public DreamService(IDreamRepository dreamRepo, TagService tagService)
	{
		_dreamRepo = dreamRepo;
		_tagService = tagService;
	}

	public Dream GetDreamById(int id)
	{
		return _dreamRepo.GetDreamById(id);
	}

	public void CreateDream(Dream dream)
	{
		_dreamRepo.CreateDream(dream);
	}

	public void DeleteDream(int id)
	{
		_dreamRepo.DeleteDream(id);
	}

	public void UpdateDream(Dream dream)
	{
		_dreamRepo.UpdateDream(dream);
	}

	public List<Dream> GetAllDreams()
	{
		return _dreamRepo.GetAllDreams();
	}
}
