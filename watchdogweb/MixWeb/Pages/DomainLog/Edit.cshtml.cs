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

namespace MixWeb.Pages.DomainLog
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
        public MdomainLog MdomainLog { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.MdomainLogs == null)
            {
                return NotFound();
            }

            var mdomainlog =  await _context.MdomainLogs.FirstOrDefaultAsync(m => m.Id == id);
            if (mdomainlog == null)
            {
                return NotFound();
            }
            MdomainLog = mdomainlog;
           ViewData["DomainId"] = new SelectList(_context.Mdomains, "Id", "Id");
           ViewData["UrserId"] = new SelectList(_context.Maccounts, "Id", "Id");
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

            _context.Attach(MdomainLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MdomainLogExists(MdomainLog.Id))
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

        private bool MdomainLogExists(long id)
        {
          return (_context.MdomainLogs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
