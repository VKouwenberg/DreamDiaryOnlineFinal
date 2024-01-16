using LogicDDO.Models;
using TestBootstrapRazorPages.AppService;
using TestBootstrapRazorPages.ViewModels;

namespace TestBootstrapRazorPages.AppService.ViewAppServicesInterfaces;

public interface ITagVMService
{
	TagVM MapTagToTagVM(Tag tag);
	List<TagVM> MapTagsToTagVMs(List<Tag> tags);
	Tag MapTagVMToTag(TagVM tagVM);
	List<Tag> MapTagVMsToTags(List<TagVM> tagVMs);
}
