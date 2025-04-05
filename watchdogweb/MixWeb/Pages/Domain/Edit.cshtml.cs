using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MixWeb.Models;

namespace MixWeb.Pages.Domain
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly MixWeb.Models.MixWebContext _context;

        public EditModel(MixWeb.Models.MixWebContext context)
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

            var mdomain =  await _context.Mdomains.FirstOrDefaultAsync(m => m.Id == id);
            if (mdomain == null)
            {
                return NotFound();
            }
            else
            {
                var watchs = _context.Mwatches.AsNoTracking()
                            .Where(m => m.DomainId == id)
                            .Select(m => m.Id)  
                            .ToList();
                
                if (watchs.Count > 0) {
                    ViewData["Url"] = "readonly";
                }
                else
                {
                    ViewData["Url"] = "";
                }
            }
            
            Mdomain = mdomain;
            ViewData["Org"] = new SelectList(_context.Morgs, "Org", "Org");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Mdomain.OrgNavigation");
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var domainToUpdate = await _context.Mdomains.FindAsync(Mdomain.Id);
            if (domainToUpdate == null)
            {
                return NotFound();
            }

            if (domainToUpdate.Wurl != Mdomain.Wurl) { domainToUpdate.Wurl = Mdomain.Wurl; }
            if (domainToUpdate.Org != Mdomain.Org) { domainToUpdate.Org = Mdomain.Org; }
            if (domainToUpdate.Wrun != Mdomain.Wrun) { domainToUpdate.Wrun = Mdomain.Wrun; }
            if (domainToUpdate.Wmemo != Mdomain.Wmemo) { domainToUpdate.Wmemo = Mdomain.Wmemo; }
     
            //_context.Attach(Mdomain).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MdomainExists(Mdomain.Id))
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

        private bool MdomainExists(int id)
        {
          return (_context.Mdomains?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
