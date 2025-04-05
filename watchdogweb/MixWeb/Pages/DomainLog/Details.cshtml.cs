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

namespace MixWeb.Pages.DomainLog
{
    [Authorize(Roles = "1")]
    public class DetailsModel : PageModel
    {
        private readonly MixWeb.Models.MixWebContext _context;

        public DetailsModel(MixWeb.Models.MixWebContext context)
        {
            _context = context;
        }

      public MdomainLog MdomainLog { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.MdomainLogs == null)
            {
                return NotFound();
            }

            var mdomainlog = await _context.MdomainLogs.FirstOrDefaultAsync(m => m.Id == id);
            if (mdomainlog == null)
            {
                return NotFound();
            }
            else 
            {
                MdomainLog = mdomainlog;
            }
            return Page();
        }
    }
}
