using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MixWeb.Models;

namespace MixWeb.Pages.Account
{
    [Authorize(Roles = "1")]
    public class CreateModel : PageModel
    {
        private readonly MixWeb.Models.MixWebContext _context;

        public CreateModel(MixWeb.Models.MixWebContext context)
        {
            _context = context;
            
        }

        public IActionResult OnGet()
        {
            ViewData["Org"] = new SelectList(_context.Morgs, "Org", "Org");
            ViewData["Role"] = new SelectList(_context.Mpermissions, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Maccount Maccount { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Maccount.RoleNavigation");
            if (!ModelState.IsValid || _context.Maccounts == null || Maccount == null)
            {
                return Page();
            }

            _context.Maccounts.Add(Maccount);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
