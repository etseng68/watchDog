using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MixWeb.Models;

namespace MixWeb.Pages.Watch
{
    public class DeleteModel : PageModel
    {
        private readonly MixWeb.Models.MixWebContext _context;

        public DeleteModel(MixWeb.Models.MixWebContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Mwatch Mwatch { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Mwatches == null)
            {
                return NotFound();
            }

            var mwatch = await _context.Mwatches.FirstOrDefaultAsync(m => m.Id == id);

            if (mwatch == null)
            {
                return NotFound();
            }
            else 
            {
                Mwatch = mwatch;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Mwatches == null)
            {
                return NotFound();
            }
            var mwatch = await _context.Mwatches.FindAsync(id);

            if (mwatch != null)
            {
                Mwatch = mwatch;
                _context.Mwatches.Remove(Mwatch);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
