using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicDDO.Models;
using LogicDDO.ModelsDataAccessDTOs;

namespace LogicDDO.Services.DataAccessRepositoriesInterfaces;

public interface IDreamRepository
{
	List<DreamDTOLogic> GetAllDreams();
	DreamDTOLogic GetDreamById(int id);
	void CreateDream(DreamDTOLogic dto);
	void UpdateDream(DreamDTOLogic dto);
	void DeleteDream(int id);
}
