﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDDO.ModelsDTO;
using DataAccessDDO.Repositories;
using LogicDDO.Models;

namespace LogicDDO.Services;

public class TagService
{
    private readonly TagRepo _tagRepo;

    public TagService(TagRepo tagRepo)
    {
        _tagRepo = tagRepo;
    }

    public Tag MapTagDTOToTag(TagDTO dto)
    {
        Tag tag = new Tag
        {
            TagId = dto.TagId,
            TagName = dto.TagName,
            RestId = dto.RestId
        };

        /*Console.WriteLine("Tag in VM mapping MapTagDTOToTag");
        Console.WriteLine(tag.TagName);*/

        return tag;
    }

    public List<Tag> MapTagDTOsToTags(List<TagDTO> dTOs)
    {
        List<Tag> tags = new List<Tag>();

        foreach (TagDTO dto in dTOs)
        {
            Tag tag = MapTagDTOToTag(dto);

            /*Console.WriteLine("Tag in VM mapping all. MapTagDTOsToTags");
            Console.WriteLine(tag.TagName);*/

            tags.Add(tag);
        }

        return tags;
    }
}
