using LogicDDO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestBootstrapRazorPages.AppService;
using TestBootstrapRazorPages.ViewModels;
using TestBootstrapRazorPages.AppService.ViewInterfaces;

namespace TestBootstrapRazorPages.Pages.DreamVMs
{
    public class DetailsDreamModel : PageModel
    {
		private readonly IDreamVMService _dreamVMService;

		public DetailsDreamModel(IDreamVMService dreamVMService)
		{
			_dreamVMService = dreamVMService;
		}

		[BindProperty]
		public DreamVM Dream { get; set; }

		public IActionResult OnGet(int id)
		{
			Dream = _dreamVMService.GetDreamById(id);
			if (Dream == null)
			{
				return NotFound();
			}

			while (Dream.Tags.Count < 3)
			{
				Dream.Tags.Add(new TagVM());
			}

			return Page();
		}

		public IActionResult OnPost()
		{
			if (Dream.Id > 0)
			{
				_dreamVMService.DeleteDream(Dream.Id);
			}

			return RedirectToPage("/DreamVMs/Index");
		}
	}
}
