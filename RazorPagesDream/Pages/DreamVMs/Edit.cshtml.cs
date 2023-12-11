/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesDream.ViewModels;

namespace RazorPagesDream.Pages.DreamVMs
{
    public class EditModel : PageModel
    {
        private readonly RazorPagesDream.Data.RazorPagesDreamContext _context;

        public EditModel(RazorPagesDream.Data.RazorPagesDreamContext context)
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

            var dreamvm =  await _context.DreamVM.FirstOrDefaultAsync(m => m.Id == id);
            if (dreamvm == null)
            {
                return NotFound();
            }
            DreamVM = dreamvm;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DreamVM).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DreamVMExists(DreamVM.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DreamVMExists(int id)
        {
          return (_context.DreamVM?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
*/