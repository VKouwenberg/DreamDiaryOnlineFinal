using LogicDDO.Models;
using TestBootstrapRazorPages.AppService;
using TestBootstrapRazorPages.ViewModels;

namespace TestBootstrapRazorPages.AppService.ViewAppServicesInterfaces;

public interface IDreamVMService
{
	List<DreamVM> GetAllDreams();
	DreamVM GetDreamById(int id);
	void CreateDreamVM(DreamVM dreamVM);
	void UpdateDream(DreamVM dreamVM);
	void DeleteDream(int id);

	//mapping
	Dream MapDreamVMToDream(DreamVM dreamVM);
	List<DreamVM> MapLogicDreamsToDreamVMs(List<Dream> dreams);
	DreamVM MapLogicDreamtoDreamVM(Dream dream);
}
