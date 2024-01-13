using LogicDDO.Services;
using TestBootstrapRazorPages.ViewModels;
using LogicDDO.Models;
using DataAccessDDO.ModelsDTO;
using LogicDDO.Services.LogicInterfaces;

namespace TestBootstrapRazorPages.AppService;

public class TagVMService : ViewInterfaces.ITagVMService
{
    private readonly ITagService _tagService;

    public TagVMService(ITagService tagService)
    {
        _tagService = tagService;
    }


    //these 2 map logic Tags to ViewModel Tags
    public TagVM MapTagToTagVM(Tag tag)
    {
        TagVM tagVM = new TagVM
        {
            TagId = tag.TagId,
            TagName = tag.TagName
        };
        return tagVM;
    }

    public List<TagVM> MapTagsToTagVMs(List<Tag> tags)
    {
        List<TagVM> tagVMs = new List<TagVM>();

        foreach (Tag tag in tags)
        {
            TagVM tagVM = MapTagToTagVM(tag);
            tagVMs.Add(tagVM);
        }

        return tagVMs;
    }

	//these 2 map ViewModel Tags to logic Tags
	public Tag MapTagVMToTag(TagVM tagVM)
    {
        Tag tag = new Tag
        {
            TagId=tagVM.TagId,
            TagName = tagVM.TagName
        };
        return tag;
    }

    public List<Tag> MapTagVMsToTags(List<TagVM> tagVMs)
    {
        List<Tag> tags = new List<Tag>();

        foreach (TagVM tagVM in tagVMs)
        {
            Tag tag = MapTagVMToTag(tagVM);
            tags.Add(tag);
        }

        return tags;
    }
}
