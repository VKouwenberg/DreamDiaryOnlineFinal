using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesDream.Data;
using RazorPagesDream.ViewModels;

using LogicDDO.Services;

namespace RazorPagesDream.Pages.DreamVMs;

public class IndexModel : PageModel
{

    private readonly LogicDDO.Services.DreamService _context;

    public IndexModel(LogicDDO.Services.DreamService context)
    {
        _context = context;
    }

    public IList<DreamVM> DreamVM { get; set; } = default!;

    public List<>

    public async Task OnGetAsync()
    {
        if (_context.DreamVM != null)
        {
            DreamVM = await _context.DreamVM.ToListAsync();
        }
    }
}

