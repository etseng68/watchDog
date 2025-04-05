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

namespace MixWeb.Pages.DomainLog
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
        ViewData["DomainId"] = new SelectList(_context.Mdomains, "Id", "Id");
        ViewData["UrserId"] = new SelectList(_context.Maccounts, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public MdomainLog MdomainLog { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.MdomainLogs == null || MdomainLog == null)
            {
                return Page();
            }

            _context.MdomainLogs.Add(MdomainLog);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
