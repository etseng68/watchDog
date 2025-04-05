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

namespace MixWeb.Pages.Org
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
      public Morg Morg { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Morgs == null)
            {
                return NotFound();
            }

            var morg = await _context.Morgs.FirstOrDefaultAsync(m => m.Id == id);

            if (morg == null)
            {
                return NotFound();
            }
            else 
            {
                Morg = morg;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Morgs == null)
            {
                return NotFound();
            }
            var morg = await _context.Morgs.FindAsync(id);

            if (morg != null)
            {
                Morg = morg;
                _context.Morgs.Remove(Morg);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
