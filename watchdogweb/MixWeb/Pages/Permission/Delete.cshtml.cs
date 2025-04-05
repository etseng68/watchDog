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

namespace MixWeb.Pages.Permission
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
      public Mpermission Mpermission { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Mpermissions == null)
            {
                return NotFound();
            }

            var mpermission = await _context.Mpermissions.FirstOrDefaultAsync(m => m.Id == id);

            if (mpermission == null)
            {
                return NotFound();
            }
            else 
            {
                Mpermission = mpermission;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Mpermissions == null)
            {
                return NotFound();
            }
            var mpermission = await _context.Mpermissions.FindAsync(id);

            if (mpermission != null)
            {
                Mpermission = mpermission;
                _context.Mpermissions.Remove(Mpermission);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
