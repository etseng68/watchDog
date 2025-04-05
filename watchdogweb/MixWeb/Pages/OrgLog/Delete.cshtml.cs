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

namespace MixWeb.Pages.OrgLog
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
      public MorgLog MorgLog { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.MorgLogs == null)
            {
                return NotFound();
            }

            var morglog = await _context.MorgLogs.FirstOrDefaultAsync(m => m.Id == id);

            if (morglog == null)
            {
                return NotFound();
            }
            else 
            {
                MorgLog = morglog;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.MorgLogs == null)
            {
                return NotFound();
            }
            var morglog = await _context.MorgLogs.FindAsync(id);

            if (morglog != null)
            {
                MorgLog = morglog;
                _context.MorgLogs.Remove(MorgLog);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
