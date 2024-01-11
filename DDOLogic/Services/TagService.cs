using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDDO.ModelsDTO;
using DataAccessDDO.Repositories;
using LogicDDO.Models;
using DataAccessDDO.Repositories.DataAccessInterfaces;

namespace LogicDDO.Services;

public class TagService : LogicInterfaces.ITagService
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

        foreach (TagDTO dto in dTOs)
        {
            Tag tag = MapTagDTOToTag(dto);
            tags.Add(tag);
        }

        return tags;
    }
	public TagDTO MapTagToTagDTO(Tag tag)
	{
		TagDTO dto = new TagDTO
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
            TagDTO dto = MapTagToTagDTO(tag);
            dTOs.Add(dto);
        }

        return dTOs;
    }
}
