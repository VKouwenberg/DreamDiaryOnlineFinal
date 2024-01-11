using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDDO.ModelsDTO;
using LogicDDO.Models;

namespace LogicDDO.Services.LogicInterfaces;

public interface ITagService
{
	Tag MapTagDTOToTag(TagDTO dto);
	List<Tag> MapTagDTOsToTags(List<TagDTO> dtos);
	TagDTO MapTagToTagDTO(Tag tag);
	List<TagDTO> MapTagsToTagDTOs(List<Tag> tags);
}
