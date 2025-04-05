using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MixWeb.Models;

namespace MixWeb.Pages.Watch
{
    public class DetailsModel : PageModel
    {
        private readonly MixWeb.Models.MixWebContext _context;

        public DetailsModel(MixWeb.Models.MixWebContext context)
        {
            _context = context;
        }

      public Mwatch Mwatch { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Mwatches == null)
            {
                return NotFound();
            }

            var mwatch = await _context.Mwatches.FirstOrDefaultAsync(m => m.Id == id);
            if (mwatch == null)
            {
                return NotFound();
            }
            else 
            {
                Mwatch = mwatch;
            }
            return Page();
        }
    }
}
