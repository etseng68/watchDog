using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MixWeb.Models;

namespace MixWeb.Pages.Domain
{
    [Authorize]
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
            return Page();
        }

        [BindProperty]
        public Mdomain Mdomain { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Mdomain.OrgNavigation");
            if (!ModelState.IsValid || _context.Mdomains == null || Mdomain == null)
            {
                return Page();
            }
            if(_context.Mdomains.Any(d => d.Wurl == Mdomain.Wurl)) {
                ModelState.AddModelError("Mdomain.Wurl", "URL 已存在，请输入其他 URL。");
                return Page();
            }
            Mdomain.Wurl = Mdomain.Wurl.Trim();
            _context.Mdomains.Add(Mdomain);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
