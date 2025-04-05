using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MixWeb.Models;
using Newtonsoft.Json;
using System.Data;

namespace MixWeb.Pages.DomainLog
{
    [Authorize(Roles = "1")]
    public class ListModel : PageModel
    {
        private readonly MixWebContext _context;

        public ListModel(MixWebContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (_context.MdomainLogs != null)
            {
                //object? domainLogs = null;

                var domainLogs = await _context.MdomainLogs.AsNoTracking()
                            .Select(d => new { d.Id,d.Domain.Wurl, d.Urser.Name, d.CreateAt, d.Note })
                            .ToArrayAsync();
                    
                return Content(JsonConvert.SerializeObject(domainLogs), "application/json");
            }
            return Content("[{\"Wurl\": \"üoŸY¡œ\"}]");
        }
    }
}
