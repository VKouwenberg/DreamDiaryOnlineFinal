using TestBootstrapRazorPages.ViewModels;

namespace TestBootstrapRazorPages.AppService.ViewInterfaces;

public interface IDreamVMService
{
	List<DreamVM> GetDreams();
	DreamVM GetDreamById(int id);
	void CreateDreamVM(DreamVM dreamVM);
	void UpdateDream(DreamVM dreamVM);
	void DeleteDream(int id);
}
