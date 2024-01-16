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

public interface ITagRepo
{
	int CreateTag(TagDTOLogic tag);
	void DeleteDreamTags(int dreamId);
	void DeleteTagsByDreamId(int dreamId);

	//mapping
	TagDTO MapTagDTOLogicToTagDTO(TagDTOLogic tag);
	List<TagDTO> MapTagDTOLogicsToTagDTOs(List<TagDTOLogic> tags);
	TagDTOLogic MapTagDTOToTagDTOLogic(TagDTO dto);
	List<TagDTOLogic> MapTagDTOsToTagDTOLogics(List<TagDTO> dTOs);
}
