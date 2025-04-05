using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MixWeb.Models;

namespace MixWeb.Pages.Domain
{
    [Authorize]
    public class AdminDelModel : PageModel
    {
        private readonly MixWeb.Models.MixWebContext _context;

        public AdminDelModel(MixWebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Mdomain Mdomain { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Mdomains == null)
            {
                return NotFound();
            }

            var mdomain = await _context.Mdomains.FirstOrDefaultAsync(m => m.Id == id);

            if (mdomain == null)
            {
                return NotFound();
            }
            else
            {
                Mdomain = mdomain;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Mdomains == null)
            {
                return NotFound();
            }
            //var mdomain = await _context.Mdomains.FindAsync(id);
            var mdomain = await _context.Mdomains
                .Include(d => d.MdomainLogs)
                .Include(d => d.Mwatches)
                .FirstOrDefaultAsync(d => d.Id == id);


            if (mdomain != null)
            {
                Mdomain = mdomain;
                _context.Mdomains.Remove(Mdomain);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
