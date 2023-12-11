using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesDream.Data;
using RazorPagesDream.ViewModels;

namespace RazorPagesDream.Pages.DreamVMs
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesDream.Data.RazorPagesDreamContext _context;

        public CreateModel(RazorPagesDream.Data.RazorPagesDreamContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DreamVM DreamVM { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.DreamVM == null || DreamVM == null)
            {
                return Page();
            }

            _context.DreamVM.Add(DreamVM);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
