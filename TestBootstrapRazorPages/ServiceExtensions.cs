using Microsoft.Extensions.DependencyInjection;
using TestBootstrapRazorPages.AppService;
using LogicDDO.Services;
using DataAccessDDO;
using DataAccessDDO.Repositories;
using DataAccessDDO.DatabaseSettings;
using LogicDDO.Services.LogicInterfaces;
using DataAccessDDO.Repositories.DataAccessInterfaces;
using TestBootstrapRazorPages.AppService.ViewInterfaces;

namespace TestBootstrapRazorPages;


//gotta inject my services into Program.cs / of Startup.cs
public static class ServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");


        //DatabaseSettings
        services.Configure<DatabaseSettings>(configuration.GetSection("ConnectionStrings"));

		//Inject both the interface and the repo itself
        //Adding it scoped means you require both to be added

		//from data access layer
		services.AddScoped<ITagRepository, TagRepo>();
		services.AddScoped<IRestRepository, RestRepo>();
		services.AddScoped<IDreamRepository, DreamRepo>();

        //from logic/business layer
        services.AddScoped<IDreamService, DreamService>();
        services.AddScoped<IRestService, RestService>();
        services.AddScoped<ITagService, TagService>();

        //from viewlayer
        services.AddScoped<IDreamVMService, DreamVMService>();
        services.AddScoped<IRestVMService, RestVMService>();
        services.AddScoped<ITagVMService, TagVMService>();
    }
}