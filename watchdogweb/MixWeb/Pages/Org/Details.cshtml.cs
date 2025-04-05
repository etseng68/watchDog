using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MixWeb.Models;

namespace MixWeb.Pages.Org
{
    public class DetailsModel : PageModel
    {
        private readonly MixWeb.Models.MixWebContext _context;

        public DetailsModel(MixWeb.Models.MixWebContext context)
        {
            _context = context;
        }

      public Morg Morg { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Morgs == null)
            {
                return NotFound();
            }

            var morg = await _context.Morgs.FirstOrDefaultAsync(m => m.Id == id);
            if (morg == null)
            {
                return NotFound();
            }
            else 
            {
                Morg = morg;
            }
            return Page();
        }
    }
}
