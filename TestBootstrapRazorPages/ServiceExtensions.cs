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
		services.AddScoped<LogicDDO.Services.DataAccessRepositoriesInterfaces.IDreamRepository, DreamRepository>();
		services.AddScoped<LogicDDO.Services.DataAccessRepositoriesInterfaces.IRestRepository, RestRepository>();
		services.AddScoped<LogicDDO.Services.DataAccessRepositoriesInterfaces.ITagRepository, TagRepository>();

		//register DataAccess repository interfaces
		services.AddScoped<DataAccessDDO.Repositories.DataAccessRepositoriesInterfaces.IDreamRepository, DreamRepository>();
		services.AddScoped<DataAccessDDO.Repositories.DataAccessRepositoriesInterfaces.IRestRepository, RestRepository>();
		services.AddScoped<DataAccessDDO.Repositories.DataAccessRepositoriesInterfaces.ITagRepository, TagRepository>();



		//register view/business appServices
		services.AddScoped<IDreamVMService, DreamVMService>();
        services.AddScoped<IRestVMService, RestVMService>();
        services.AddScoped<ITagVMService, TagVMService>();
    }
}