using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDDO.Repositories;
using DataAccessDDO.ModelsDTO;
using LogicDDO.Models;
using LogicDDO.ModelsDataAccessDTOs;

namespace DataAccessDDO.Repositories.DataAccessRepositoriesInterfaces;

public interface IDreamRepo
{
	List<DreamDTOLogic> GetAllDreams();
	void CreateDream(DreamDTOLogic dream);
	void UpdateDream(DreamDTOLogic dream);
	void DeleteDream(int dreamId);
	DreamDTOLogic GetDreamById(int dreamId);

	//mapping
	DreamDTO MapDreamDTOLogicToDreamDTO(DreamDTOLogic dream);
	List<DreamDTO> MapDreamDTOLogicsToDreamDTOs(List<DreamDTOLogic> dreams);
	DreamDTOLogic MapDreamDTOToDreamDTOLogic(DreamDTO dto);
	List<DreamDTOLogic> MapDreamDTOsToDreamDTOLogics(List<DreamDTO> dreamDTOs);
}
