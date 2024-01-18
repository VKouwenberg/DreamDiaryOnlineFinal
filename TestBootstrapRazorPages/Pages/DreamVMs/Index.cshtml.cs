using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestBootstrapRazorPages.ViewModels;
using TestBootstrapRazorPages.AppService;
using TestBootstrapRazorPages.AppService.ViewAppServicesInterfaces;
using MySql.Data.MySqlClient;

namespace TestBootstrapRazorPages.Pages.DreamVMs;

public class IndexModel : PageModel
{
    private readonly IDreamVMService _dreamVMService;

    [TempData]
    public string ErrorMessage {  get; set; }

    public IndexModel(IDreamVMService context)
    {
        _dreamVMService = context;
    }

    //[BindProperty]
    public List<DreamVM> Dreams { get; set; }

    public IActionResult OnGet()
    {
        try
        {
            Dreams = _dreamVMService.GetAllDreams();
        }
        catch (Exception ex)
        {
            //ErrorMessage = "Oepsie woepsie. Geen connectie-weksie met de daattie-waattie-base.";
            ErrorMessage = "Helaas geen connectie kunnen maken met de database. Tragisch";
            RedirectToPage("/Error", new { ErrorMessage });
        }

        return Page();
    }

	/*public IActionResult OnGet()
	{
		try
		{
			Dreams = _dreamVMService.GetAllDreams();
			return Page();
		}
		catch (MySqlException ex)
		{
			_logger.LogError(ex, "An error occurred while connecting to the database.");

            TempData["ErrorMessage"] = "Error error terror tremor";

			return RedirectToPage("/_Error");
		}
	}*/
	/*public IActionResult OnGet()
    {
        Dreams = _dreamVMService.GetAllDreams();
        return Page();
    }*/

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

