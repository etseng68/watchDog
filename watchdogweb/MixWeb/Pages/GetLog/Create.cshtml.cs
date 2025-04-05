using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MixWeb.Models;

namespace MixWeb.Pages.GetLog
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
        public MgetLog MgetLog { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.MgetLogs == null || MgetLog == null)
            {
                return Page();
            }

            _context.MgetLogs.Add(MgetLog);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
