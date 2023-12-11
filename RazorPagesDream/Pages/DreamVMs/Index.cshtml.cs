using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesDream.ViewModels;
using RazorPagesDream.AppService;

using LogicDDO.Services;

namespace RazorPagesDream.Pages.DreamVMs;

public class IndexModel : PageModel
{
    private readonly DreamVMService _dreamVMService;

    public IndexModel(DreamVMService context)
    {
        _dreamVMService = context;
    }

    public List<DreamVM> Dreams { get; set; }

    public void OnGet()
    {
        Dreams = _dreamVMService.GetDreams();
    }


    /*public IList<DreamVM> DreamVM { get; set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.DreamVM != null)
        {
            DreamVM = await _context.DreamVM.ToListAsync();
        }
    }*/
}

