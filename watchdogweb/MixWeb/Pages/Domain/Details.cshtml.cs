using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MixWeb.Models;

namespace MixWeb.Pages.Domain
{
    public class DetailsModel : PageModel
    {
        private readonly MixWeb.Models.MixWebContext _context;

        public DetailsModel(MixWeb.Models.MixWebContext context)
        {
            _context = context;
        }

      public Mdomain Mdomain { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Mdomains == null)
            {
                return NotFound();
            }

            var mdomain = await _context.Mdomains.FirstOrDefaultAsync(m => m.Id == id);
            if (mdomain == null)
            {
                return NotFound();
            }
            else 
            {
                Mdomain = mdomain;
            }
            return Page();
        }
    }
}
