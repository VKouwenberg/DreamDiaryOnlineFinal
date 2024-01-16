using LogicDDO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicDDO.ModelsDataAccessDTOs;

namespace LogicDDO.Services.LogicServicesInterfaces;

public interface IDreamService
{
	List<Dream> GetAllDreams();
	Dream GetDreamById(int id);
	void CreateDream(Dream dream);
	void DeleteDream(int id);
	void UpdateDream(Dream dream);

	//mapping
	Dream MapDreamDTOLogicToDream(DreamDTOLogic dreamDTO);
	List<Dream> MapDreamDTOLogicsToDreams(List<DreamDTOLogic> dreamDTOs);
	DreamDTOLogic MapDreamToDreamDTOLogic(Dream dream);
	List<DreamDTOLogic> MapDreamsToDreamDTOLogics(List<Dream> dreams);
}
