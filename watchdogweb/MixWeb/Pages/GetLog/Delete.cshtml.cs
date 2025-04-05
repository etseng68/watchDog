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

namespace MixWeb.Pages.GetLog
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
      public MgetLog MgetLog { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.MgetLogs == null)
            {
                return NotFound();
            }

            var mgetlog = await _context.MgetLogs.FirstOrDefaultAsync(m => m.Id == id);

            if (mgetlog == null)
            {
                return NotFound();
            }
            else 
            {
                MgetLog = mgetlog;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null || _context.MgetLogs == null)
            {
                return NotFound();
            }
            var mgetlog = await _context.MgetLogs.FindAsync(id);

            if (mgetlog != null)
            {
                MgetLog = mgetlog;
                _context.MgetLogs.Remove(MgetLog);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
