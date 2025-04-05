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

namespace MixWeb.Pages.DomainLog
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
      public MdomainLog MdomainLog { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.MdomainLogs == null)
            {
                return NotFound();
            }

            var mdomainlog = await _context.MdomainLogs.FirstOrDefaultAsync(m => m.Id == id);

            if (mdomainlog == null)
            {
                return NotFound();
            }
            else 
            {
                MdomainLog = mdomainlog;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null || _context.MdomainLogs == null)
            {
                return NotFound();
            }
            var mdomainlog = await _context.MdomainLogs.FindAsync(id);

            if (mdomainlog != null)
            {
                MdomainLog = mdomainlog;
                _context.MdomainLogs.Remove(MdomainLog);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
