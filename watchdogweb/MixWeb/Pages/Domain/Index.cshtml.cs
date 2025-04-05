using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MixWeb.Models;

namespace MixWeb.Pages.Domain
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly MixWeb.Models.MixWebContext _context;

        public IndexModel(MixWeb.Models.MixWebContext context)
        {
            _context = context;
        }

        public IList<Mdomain> Mdomain { get;set; } = default!;

        public async Task OnGetAsync()
        {
            string userName = this.User.FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
            Maccount maccount = await _context.Maccounts.AsNoTracking()
                .Where(a => a.Name == userName)
                .SingleOrDefaultAsync();
            if (_context.Mdomains != null)
            {
                if (maccount.Role == 2)
                {
                    Mdomain = await _context.Mdomains
                         .Include(m => m.OrgNavigation)
                         .Where(d => d.Org == maccount.Org)
                         .ToListAsync();
                }
                else
                {
                    Mdomain = await _context.Mdomains
                        .Include(m => m.OrgNavigation)
                        .ToListAsync();
                }
            }
        }
    }
}
