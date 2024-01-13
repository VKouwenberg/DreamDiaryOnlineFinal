using LogicDDO.Models;
using TestBootstrapRazorPages.ViewModels;

namespace TestBootstrapRazorPages.AppService.ViewInterfaces;

public interface ITagVMService
{
	public TagVM MapTagToTagVM(Tag tag);
	public List<TagVM> MapTagsToTagVMs(List<Tag> tags);
	public Tag MapTagVMToTag(TagVM tagVM);
	public List<Tag> MapTagVMsToTags(List<TagVM> tagVMs);
}

