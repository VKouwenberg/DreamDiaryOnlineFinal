using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestBootstrapRazorPages.ViewModels;
using TestBootstrapRazorPages.AppService;
using TestBootstrapRazorPages.AppService.ViewInterfaces;

namespace TestBootstrapRazorPages.Pages.DreamVMs
{
    public class EditDreamModel : PageModel
    {
        private readonly IDreamVMService _dreamVMService;

        public EditDreamModel(IDreamVMService dreamVMService)
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
            Dream.Tags = Dream.Tags.Where(tag => !string.IsNullOrWhiteSpace(tag.TagName)).ToList();

            _dreamVMService.UpdateDream(Dream);
            return RedirectToPage("/DreamVMs/Index");
        }
    }
}
