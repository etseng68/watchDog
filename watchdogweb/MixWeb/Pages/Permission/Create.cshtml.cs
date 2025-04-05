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

namespace MixWeb.Pages.Permission
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
            return Page();
        }

        [BindProperty]
        public Mpermission Mpermission { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Mpermissions == null || Mpermission == null)
            {
                return Page();
            }

            _context.Mpermissions.Add(Mpermission);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
