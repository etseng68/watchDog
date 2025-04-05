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

namespace MixWeb.Pages.OrgLog
{
    [Authorize(Roles = "1")]
    public class IndexModel : PageModel
    {
        private readonly MixWeb.Models.MixWebContext _context;

        public IndexModel(MixWeb.Models.MixWebContext context)
        {
            _context = context;
        }

        public IList<MorgLog> MorgLog { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.MorgLogs != null)
            {
                MorgLog = await _context.MorgLogs
                .Include(m => m.Org)
                .Include(m => m.User)
                .ToListAsync();
            }
        }
    }
}
