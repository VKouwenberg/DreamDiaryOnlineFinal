using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestBootstrapRazorPages.ViewModels;
using TestBootstrapRazorPages.AppService;

namespace TestBootstrapRazorPages.Pages.DreamVMs;

public class IndexModel : PageModel
{
    private readonly DreamVMService _dreamVMService;

    public IndexModel(DreamVMService context)
    {
        _dreamVMService = context;
    }

    [BindProperty]
    public List<DreamVM> Dreams { get; set; }


    public IActionResult OnGet()
    {
        Dreams = _dreamVMService.GetAllDreams();
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

