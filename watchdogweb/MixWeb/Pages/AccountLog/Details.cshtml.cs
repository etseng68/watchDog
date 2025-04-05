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

namespace MixWeb.Pages.AccountLog
{
    [Authorize(Roles = "1")]
    public class DetailsModel : PageModel
    {
        private readonly MixWeb.Models.MixWebContext _context;

        public DetailsModel(MixWeb.Models.MixWebContext context)
        {
            _context = context;
        }

      public MaccessLog MaccessLog { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.MaccessLogs == null)
            {
                return NotFound();
            }

            var maccesslog = await _context.MaccessLogs.FirstOrDefaultAsync(m => m.Id == id);
            if (maccesslog == null)
            {
                return NotFound();
            }
            else 
            {
                MaccessLog = maccesslog;
            }
            return Page();
        }
    }
}
