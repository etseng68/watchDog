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
    public class IndexModel : PageModel
    {
        private readonly MixWeb.Models.MixWebContext _context;

        public IndexModel(MixWeb.Models.MixWebContext context)
        {
            _context = context;
        }

        public IList<MdomainLog> MdomainLog { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.MdomainLogs != null)
            {
                MdomainLog = await _context.MdomainLogs
                .Include(m => m.Domain)
                .Include(m => m.Urser).ToListAsync();
            }
        }
    }
}
