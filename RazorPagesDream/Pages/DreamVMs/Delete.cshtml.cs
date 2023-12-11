using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesDream.Data;
using RazorPagesDream.ViewModels;

namespace RazorPagesDream.Pages.DreamVMs
{
    public class DeleteModel : PageModel
    {
        private readonly RazorPagesDream.Data.RazorPagesDreamContext _context;

        public DeleteModel(RazorPagesDream.Data.RazorPagesDreamContext context)
        {
            _context = context;
        }

        [BindProperty]
      public DreamVM DreamVM { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.DreamVM == null)
            {
                return NotFound();
            }

            var dreamvm = await _context.DreamVM.FirstOrDefaultAsync(m => m.Id == id);

            if (dreamvm == null)
            {
                return NotFound();
            }
            else 
            {
                DreamVM = dreamvm;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.DreamVM == null)
            {
                return NotFound();
            }
            var dreamvm = await _context.DreamVM.FindAsync(id);

            if (dreamvm != null)
            {
                DreamVM = dreamvm;
                _context.DreamVM.Remove(DreamVM);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
