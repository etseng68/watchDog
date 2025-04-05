using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MixWeb.Models;
using Newtonsoft.Json;
using System.Data;

namespace MixWeb.Pages.GetLog
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
            if (_context.Mwatches != null)
            {
                DateTime eDay = DateTime.Now;
                DateTime sDay = eDay.AddDays(-1);
                var getLog = await _context.MgetLogs.AsNoTracking()
                        .Where(g => g.ModiyAt>= sDay && g.ModiyAt<= eDay)
                        .Select(w => new { w.Id, w.Note, w.ModiyAt })
                        .OrderByDescending(w => w.ModiyAt)
                        .ToArrayAsync();
                return Content(JsonConvert.SerializeObject(getLog), "application/json");

            }
            return Content("[{\"Status\": \"ΩÒ»’üoŸY¡œ\",\"Wtime\": \"\"}]");
        }
       
    }
}
