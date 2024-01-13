using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestBootstrapRazorPages.ViewModels;
using TestBootstrapRazorPages.AppService;
using TestBootstrapRazorPages.AppService.ViewInterfaces;

namespace TestBootstrapRazorPages.Pages.DreamVMs;

public class IndexModel : PageModel
{
    private readonly IDreamVMService _dreamVMService;

    public IndexModel(IDreamVMService context)
    {
        _dreamVMService = context;
    }

    [BindProperty]
    public List<DreamVM> Dreams { get; set; }


    public IActionResult OnGet()
    {
        Dreams = _dreamVMService.GetDreams();
        return Page();
    }

    public IActionResult OnGetCreate()
    {
        return RedirectToPage("/DreamVMs/Create");
    }

    public IActionResult OnPostDelete(int id)
    {
        _dreamVMService.DeleteDream(id);
        return RedirectToPage();
	}
}

