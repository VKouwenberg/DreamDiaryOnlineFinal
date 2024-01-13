using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestBootstrapRazorPages.AppService;
using TestBootstrapRazorPages.ViewModels;
using TestBootstrapRazorPages.AppService.ViewInterfaces;

namespace TestBootstrapRazorPages.Pages.DreamVMs
{
    public class CreateDreamModel : PageModel
    {
        public void OnGet()
        {
        }
		private readonly IDreamVMService _dreamVMService;

		public CreateDreamModel(IDreamVMService dreamVMService)
		{
			_dreamVMService = dreamVMService;
		}

		[BindProperty]
		public DreamVM DreamVM { get; set; }

		public IActionResult OnPost()
		{
			//reads out the tag forms and creates new tags
			DreamVM.Tags = new List<TagVM>();

			for (int i = 0; i < 3; i++)
			{
				if (HttpContext.Request.Form.TryGetValue($"DreamVM.Tags[{i}].TagName", out Microsoft.Extensions.Primitives.StringValues tagValue))
				{
					DreamVM.Tags.Add(new TagVM { TagName = tagValue.ToString() });
				}
			}

            

            _dreamVMService.CreateDreamVM(DreamVM);

			

            return RedirectToPage("/DreamVMs/Index");
		}
	}
}
