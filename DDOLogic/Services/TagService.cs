using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicDDO.Models;
using LogicDDO.Services.DataAccessRepositoriesInterfaces;
using LogicDDO.Services.LogicServicesInterfaces;
using LogicDDO.ModelsDataAccessDTOs;

namespace LogicDDO.Services;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepo;

    public TagService(ITagRepository tagRepo)
    {
        _tagRepo = tagRepo;
    }

	public int CreateTag(Tag tag)
	{
		TagDTOLogic tagDTO = MapTagToTagDTOLogic(tag);

		return _tagRepo.CreateTag(tagDTO);
	}

	public void DeleteDreamTags(int dreamId)
	{
		_tagRepo.DeleteDreamTags(dreamId);
	}

	public void DeleteTagsByDreamId(int dreamId)
	{
		_tagRepo.DeleteTagsByDreamId(dreamId);
	}

	//mapping
	public Tag MapTagDTOLogicToTag(TagDTOLogic tagDTO)
	{
		Tag tag = new Tag
		{
			TagId = tagDTO.TagId,
			TagName = tagDTO.TagName
		};
		return tag;
	}

	public List<Tag> MapTagDTOLogicsToTags(List<TagDTOLogic> tagDTOs)
	{
		List<Tag> tags = new List<Tag>();
		foreach (TagDTOLogic tagDTO in tagDTOs)
		{
			Tag tag = MapTagDTOLogicToTag(tagDTO);
			tags.Add(tag);
		}
		return tags;
	}

	public TagDTOLogic MapTagToTagDTOLogic(Tag tag)
	{
		TagDTOLogic tagDTO = new TagDTOLogic
		{
			TagId = tag.TagId,
			TagName = tag.TagName
		};
		return tagDTO;
	}

	public List<TagDTOLogic> MapTagsToTagDTOLogics(List<Tag> tags)
	{
		List<TagDTOLogic> tagDTOs = new List<TagDTOLogic>();
		foreach ( Tag tag in tags)
		{
			TagDTOLogic tagDTO = MapTagToTagDTOLogic(tag);
			tagDTOs.Add(tagDTO);
		}
		return tagDTOs;
	}
}
