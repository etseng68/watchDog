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

namespace MixWeb.Pages.OrgLog
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
        public MorgLog MorgLog { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.MorgLogs == null)
            {
                return NotFound();
            }

            var morglog =  await _context.MorgLogs.FirstOrDefaultAsync(m => m.Id == id);
            if (morglog == null)
            {
                return NotFound();
            }
            MorgLog = morglog;
           ViewData["OrgId"] = new SelectList(_context.Maccounts, "Id", "Id");
           ViewData["OrgId"] = new SelectList(_context.Morgs, "Id", "Org");
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

            _context.Attach(MorgLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MorgLogExists(MorgLog.Id))
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

        private bool MorgLogExists(int id)
        {
          return (_context.MorgLogs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
