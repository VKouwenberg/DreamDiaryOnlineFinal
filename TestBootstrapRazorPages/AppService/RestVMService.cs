using LogicDDO.Services;
using TestBootstrapRazorPages.ViewModels;
using LogicDDO.Models;
using LogicDDO.Services.LogicInterfaces;

namespace TestBootstrapRazorPages.AppService;

public class RestVMService : ViewInterfaces.IRestVMService
{
    private readonly IRestService _restService;

    public RestVMService(IRestService restService)
    {
        _restService = restService;
    }


}
