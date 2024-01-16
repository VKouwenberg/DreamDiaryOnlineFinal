using LogicDDO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicDDO.ModelsDataAccessDTOs;

namespace LogicDDO.Services.LogicServicesInterfaces;

public interface ITagService
{
	int CreateTag(Tag tag);
	void DeleteDreamTags(int dreamId);
	void DeleteTagsByDreamId(int dreamId);

	//mapping
	Tag MapTagDTOLogicToTag(TagDTOLogic tagDTO);
	public List<Tag> MapTagDTOLogicsToTags(List<TagDTOLogic> tagDTOs);
	TagDTOLogic MapTagToTagDTOLogic(Tag tag);
	List<TagDTOLogic> MapTagsToTagDTOLogics(List<Tag> tags);
}
