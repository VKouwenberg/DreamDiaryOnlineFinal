using Microsoft.Extensions.DependencyInjection;
using LogicDDO.Services;
using LogicDDO.Services.DataAccessRepositoriesInterfaces;
using LogicDDO.Services.LogicServicesInterfaces;
using DataAccessDDO;
using DataAccessDDO.Repositories;
using DataAccessDDO.DatabaseSettings;
using DataAccessDDO.Repositories.DataAccessRepositoriesInterfaces;
using TestBootstrapRazorPages.AppService;
using TestBootstrapRazorPages.AppService.ViewAppServicesInterfaces;

namespace TestBootstrapRazorPages;


//gotta inject my services into Program.cs / of Startup.cs
public static class ServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection");


        //DatabaseSettings
        services.Configure<DatabaseSettings>(configuration.GetSection("ConnectionStrings"));

		//register logic layer
		services.AddScoped<IDreamService, DreamService>();
		services.AddScoped<IRestService, RestService>();
		services.AddScoped<ITagService, TagService>();

		//register DataAccess repositories
		services.AddScoped<IDreamRepository, DreamRepo>();
		services.AddScoped<IRestRepository, RestRepo>();
		services.AddScoped<ITagRepository, TagRepo>();

		//register DataAccess repository interfaces
		services.AddScoped<IDreamRepo, DreamRepo>();
		services.AddScoped<IRestRepo, RestRepo>();
		services.AddScoped<ITagRepo, TagRepo>();



		//register view/business appServices
		services.AddScoped<IDreamVMService, DreamVMService>();
        services.AddScoped<IRestVMService, RestVMService>();
        services.AddScoped<ITagVMService, TagVMService>();
    }
}