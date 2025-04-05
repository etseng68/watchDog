using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MixWeb.Models;

namespace MixWeb.Pages.Domain
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly MixWeb.Models.MixWebContext _context;

        public DeleteModel(MixWeb.Models.MixWebContext context)
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
            var mdomain = await _context.Mdomains.FindAsync(id);

            if (mdomain != null)
            {
                mdomain.Del = true;
                Mdomain = mdomain;
                //_context.Mdomains.Remove(Mdomain);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
