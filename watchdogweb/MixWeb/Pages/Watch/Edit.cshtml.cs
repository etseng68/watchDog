using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MixWeb.Models;

namespace MixWeb.Pages.Watch
{
    public class EditModel : PageModel
    {
        private readonly MixWeb.Models.MixWebContext _context;

        public EditModel(MixWeb.Models.MixWebContext context)
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

            var mwatch =  await _context.Mwatches.FirstOrDefaultAsync(m => m.Id == id);
            if (mwatch == null)
            {
                return NotFound();
            }
            Mwatch = mwatch;
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

            _context.Attach(Mwatch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MwatchExists(Mwatch.Id))
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

        private bool MwatchExists(long id)
        {
          return (_context.Mwatches?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
