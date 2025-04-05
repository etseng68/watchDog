using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MixWeb.Models;

namespace MixWeb.Pages.Account
{
    [Authorize(Roles = "1")]
    public class DeleteModel : PageModel
    {
        private readonly MixWeb.Models.MixWebContext _context;

        public DeleteModel(MixWeb.Models.MixWebContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Maccount Maccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Maccounts == null)
            {
                return NotFound();
            }

            var maccount = await _context.Maccounts.FirstOrDefaultAsync(m => m.Id == id);

            if (maccount == null)
            {
                return NotFound();
            }
            else 
            {
                Maccount = maccount;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Maccounts == null)
            {
                return NotFound();
            }
            var maccount = await _context.Maccounts.FindAsync(id);

            if (maccount != null)
            {
                Maccount = maccount;
                _context.Maccounts.Remove(Maccount);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
