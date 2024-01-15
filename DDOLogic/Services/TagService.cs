using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicDDO.Models;
using LogicDDO.Services.DataAccessRepositoriesInterfaces;

namespace LogicDDO.Services;

public class TagService : ITagRepository
{
    private readonly ITagRepository _tagRepo;

    public TagService(ITagRepository tagRepo)
    {
        _tagRepo = tagRepo;
    }

    public Tag MapTagDTOToTag(TagDTO dto)
    {
        Tag tag = new Tag
        {
            TagId = dto.TagId,
            TagName = dto.TagName
        };

        return tag;
    }

    public List<Tag> MapTagDTOsToTags(List<TagDTO> dTOs)
    {
        List<Tag> tags = new List<Tag>();

        foreach (ITagDTO dto in dTOs)
        {
            Tag tag = MapTagDTOToTag(dto);
            tags.Add(tag);
        }

        return tags;
    }
	public TagDTO MapTagToTagDTO(Tag tag)
	{
		TagDTO dto = new LogicDDO.Models.DataAccessModelInterfaces.ITagDTO
		{
			TagId = tag.TagId,
			TagName = tag.TagName
		};

		return dto;
	}

	public List<TagDTO> MapTagsToTagDTOs(List<Tag> tags)
    {
        List<TagDTO> dTOs = new List<TagDTO>();

        foreach (Tag tag in tags)
        {
            ITagDTO dto = MapTagToTagDTO(tag);
            dTOs.Add(dto);
        }

        return dTOs;
    }

	public int CreateTag(TagDTO dto)
	{
		return _tagRepo.CreateTag(dto);
	}

	public void DeleteDreamTags(int dreamId)
	{
		_tagRepo.DeleteDreamTags(dreamId);
	}

	public void DeleteTagsByDreamId(int dreamId)
	{
		_tagRepo.DeleteTagsByDreamId(dreamId);
	}
}
