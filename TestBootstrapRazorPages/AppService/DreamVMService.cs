using LogicDDO.Services;
using TestBootstrapRazorPages.ViewModels;
using LogicDDO.Models;
using Microsoft.AspNetCore.Mvc;
using TestBootstrapRazorPages.AppService.ViewInterfaces;
using LogicDDO.Services.LogicInterfaces;

namespace TestBootstrapRazorPages.AppService;

public class DreamVMService : ViewInterfaces.IDreamVMService
{
    private readonly IDreamService _dreamService;
    private readonly ITagVMService _tagVMService;
	private readonly ITagService _tagService;

	public DreamVMService(IDreamService dreamService, ITagVMService tagVMService, ITagService tagService)
    {
        _dreamService = dreamService;
        _tagVMService = tagVMService;
        _tagService = tagService;
    }

    

    //Calls private mapping methods to create viemodels of DreamVM
    public List<DreamVM> GetDreams()
    {
        List<DreamVM> dreamVMs = MapLogicDreamsToDreamVMs(_dreamService.GetDreams());

        return dreamVMs;
    }

    public DreamVM GetDreamById(int id)
    {
        Dream dream = _dreamService.GetDreamById(id);
        DreamVM dreamVM = MapLogicDreamtoDreamVM(dream);
        return dreamVM;
    }
    
    //accessible method that you can call to create a new dream in the database.
    //This converts the ViewModel to a model and passes it on to the logic layer
    public void CreateDreamVM(DreamVM dreamVM)
    {
        Dream dream = MapDreamVMToDream(dreamVM);

        _dreamService.CreateDream(dream);
    }

    public void UpdateDream(DreamVM dreamVM)
    {
        Dream dream = MapDreamVMToDream(dreamVM);

        _dreamService.UpdateDream(dream);
    }

    public void DeleteDream(int id)
    {
        _dreamService.DeleteDream(id);
    }


    //private method that controls the conversion to only happen here
    private Dream MapDreamVMToDream(DreamVM dreamVM)
    {
        Dream dream = new Dream
        {
            DreamId = dreamVM.Id,
            DreamName = dreamVM.DreamName,
            ReadableBy = dreamVM.ReadableBy,
            DreamText= dreamVM.DreamText
        };
        if ( dreamVM.Tags != null && dreamVM.Tags.Any())
        {
            dream.Tags = _tagVMService.MapTagVMsToTags(dreamVM.Tags);
        }

        return dream;
    }

	//2 methods convert logic models to ViewModels
	private List<DreamVM> MapLogicDreamsToDreamVMs(List<Dream> dreams)
	{


		List<DreamVM> dreamVMs = new List<DreamVM>();

		foreach (Dream dream in dreams)
		{
			DreamVM dreamVM = MapLogicDreamtoDreamVM(dream);
			dreamVMs.Add(dreamVM);
		}



		return dreamVMs;
	}

	private DreamVM MapLogicDreamtoDreamVM(Dream dream)
	{
		DreamVM dreamVM = new DreamVM
		{
			Id = dream.DreamId,
			DreamName = dream.DreamName,
			DreamText = dream.DreamText,
			ReadableBy = dream.ReadableBy,
            Tags = new List<TagVM>()
		};
		if (dream.Tags != null && dream.Tags.Any())
		{
			dreamVM.Tags = _tagVMService.MapTagsToTagVMs(dream.Tags);
		}


		return dreamVM;
	}
}

