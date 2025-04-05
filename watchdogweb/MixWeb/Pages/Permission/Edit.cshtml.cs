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

namespace MixWeb.Pages.Permission
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
        public Mpermission Mpermission { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Mpermissions == null)
            {
                return NotFound();
            }

            var mpermission =  await _context.Mpermissions.FirstOrDefaultAsync(m => m.Id == id);
            if (mpermission == null)
            {
                return NotFound();
            }
            Mpermission = mpermission;
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

            _context.Attach(Mpermission).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MpermissionExists(Mpermission.Id))
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

        private bool MpermissionExists(int id)
        {
          return (_context.Mpermissions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
