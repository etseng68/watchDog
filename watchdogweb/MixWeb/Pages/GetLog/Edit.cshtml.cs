using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MixWeb.Models;

namespace MixWeb.Pages.GetLog
{
    [Authorize(Roles = "1")]
    public class EditModel : PageModel
    {
        private readonly MixWeb.Models.MixWebContext _context;

        public EditModel(MixWeb.Models.MixWebContext context)
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

            var mgetlog =  await _context.MgetLogs.FirstOrDefaultAsync(m => m.Id == id);
            if (mgetlog == null)
            {
                return NotFound();
            }
            MgetLog = mgetlog;
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

            _context.Attach(MgetLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MgetLogExists(MgetLog.Id))
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

        private bool MgetLogExists(long id)
        {
          return (_context.MgetLogs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
