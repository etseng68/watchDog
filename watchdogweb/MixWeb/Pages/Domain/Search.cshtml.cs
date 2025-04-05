using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MixWeb.Models;
using Newtonsoft.Json;
using System.Data;

namespace MixWeb.Pages.Domain
{
    [Authorize(Roles = "1")]
    public class SearchModel : PageModel
    {
        private readonly MixWebContext _context;

        public SearchModel(MixWebContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            if (_context.Mdomains != null)
            {
                var domains = await _context.Mdomains.AsNoTracking()
                          .Select(d => new { d.Id, d.Wurl, d.Wrun, d.Org, d.Del, d.ModifyAt, d.Wmemo })
                          .ToArrayAsync();


                return Content(JsonConvert.SerializeObject(domains), "application/json");
            }
            return Content("[{\"Wurl\": \"üoŸY¡œ\"}]");
        }
    }
}
