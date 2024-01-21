using LogicDDO.Services;
using TestBootstrapRazorPages.ViewModels;
using LogicDDO.Models;
using LogicDDO.Services.LogicServicesInterfaces;
using LogicDDO.ModelsDataAccessDTOs;
using TestBootstrapRazorPages.AppService.ViewAppServicesInterfaces;
using TestBootstrapRazorPages.AppService;
using Microsoft.AspNetCore.Mvc;

namespace TestBootstrapRazorPages.AppService;

public class DreamVMService : IDreamVMService
{
    private readonly IDreamService _dreamService;
    private readonly ITagVMService _tagVMService;

	public DreamVMService(IDreamService dreamService, ITagVMService tagVMService)
    {
        _dreamService = dreamService;
        _tagVMService = tagVMService;
    }

    

    //Calls mapping methods to create viemodels of DreamVM
    public List<DreamVM> GetAllDreams()
    {
        List<Dream> dreams = new List<Dream>();
        List<DreamVM> dreamVMs = new List<DreamVM>();

        dreams = _dreamService.GetAllDreams();
        dreamVMs = MapLogicDreamsToDreamVMs(dreams);

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
    public Dream MapDreamVMToDream(DreamVM dreamVM)
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
	public List<DreamVM> MapLogicDreamsToDreamVMs(List<Dream> dreams)
	{


		List<DreamVM> dreamVMs = new List<DreamVM>();

		foreach (Dream dream in dreams)
		{
			DreamVM dreamVM = MapLogicDreamtoDreamVM(dream);
			dreamVMs.Add(dreamVM);
		}



		return dreamVMs;
	}

	public DreamVM MapLogicDreamtoDreamVM(Dream dream)
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

