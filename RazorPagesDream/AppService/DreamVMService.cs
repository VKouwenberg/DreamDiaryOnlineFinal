using LogicDDO.Services;
using RazorPagesDream.ViewModels;
using LogicDDO.Models;

namespace RazorPagesDream.AppService;

public class DreamVMService
{
    private readonly DreamService _dreamService;

    public DreamVMService(DreamService dreamService)
    {
        _dreamService = dreamService;
    }

    private DreamVM ConvertLogicDreamtoDreamVM(Dream dream)
    {
        DreamVM dreamVM = new DreamVM
        {
            Id = dream.DreamerId,
            DreamName = dream.DreamName,
            DreamText = dream.DreamText,
            ReadableBy = dream.ReadableBy,
            DreamerId = dream.DreamerId
        };

        return dreamVM;
    }

    //converts DreamLogicModels to viewmodels
    public List<DreamVM> GetDreams() 
    {
        List<Dream> dreams = _dreamService.GetDreams();

        List<DreamVM> dreamVMs = new List<DreamVM>();

        foreach (Dream dream in dreams)
        {
            DreamVM dreamVM = ConvertLogicDreamtoDreamVM(dream);
            dreamVMs.Add(dreamVM);
        }

        return dreamVMs;
    }
}

