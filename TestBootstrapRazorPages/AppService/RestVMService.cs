using LogicDDO.Services;
using TestBootstrapRazorPages.ViewModels;
using LogicDDO.Models;

namespace TestBootstrapRazorPages.AppService;

public class RestVMService
{
    private readonly RestService _restService;

    public RestVMService(RestService restService)
    {
        _restService = restService;
    }


}
