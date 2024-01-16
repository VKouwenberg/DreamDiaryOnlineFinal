using LogicDDO.Services;
using LogicDDO.Models;
using LogicDDO.Services.LogicServicesInterfaces;
using TestBootstrapRazorPages.AppService.ViewAppServicesInterfaces;
using TestBootstrapRazorPages.ViewModels;

namespace TestBootstrapRazorPages.AppService;

public class RestVMService : IRestVMService
{
    private readonly IRestService _restService;

    public RestVMService(IRestService restService)
    {
        _restService = restService;
    }


}
