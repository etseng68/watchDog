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

namespace MixWeb.Pages.Account
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
        public Maccount Maccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Maccounts == null)
            {
                return NotFound();
            }

            var maccount =  await _context.Maccounts.FirstOrDefaultAsync(m => m.Id == id);
            if (maccount == null)
            {
                return NotFound();
            }
            Maccount = maccount;
            ViewData["Org"] = new SelectList(_context.Morgs, "Org", "Org");
            ViewData["Role"] = new SelectList(_context.Mpermissions, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Maccount.RoleNavigation");
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var accountToUpdate = await _context.Maccounts.FindAsync(Maccount.Id);
            if (accountToUpdate == null)
            {
                return NotFound();
            }

            if(accountToUpdate.Name != Maccount.Name) { accountToUpdate.Name = Maccount.Name; }
            if(accountToUpdate.Password != Maccount.Password) { accountToUpdate.Password = Maccount.Password; }
            if(accountToUpdate.Email != Maccount.Email) { accountToUpdate.Email = Maccount.Email; }
            if(accountToUpdate.Role != Maccount.Role) { accountToUpdate.Role = Maccount.Role; }
            if(accountToUpdate.Org != Maccount.Org) { accountToUpdate.Org = Maccount.Org; }
            if(accountToUpdate.Note != Maccount.Note) { accountToUpdate.Note = Maccount.Note; }


            //_context.Attach(Maccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaccountExists(Maccount.Id))
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

        private bool MaccountExists(int id)
        {
          return (_context.Maccounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
