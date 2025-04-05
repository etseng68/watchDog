using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MixWeb.Models;
using Newtonsoft.Json;
using System.Security.Policy;

namespace MixWeb.Pages.Watch
{
    [Authorize]
    public class ListModel : PageModel
    {
		private readonly MixWebContext _context;

		public ListModel(MixWebContext context)
		{
			_context = context;
		}
		//public IList<Mwatch> Mwatch { get; set; } = default!;
		public async Task<IActionResult> OnGetAsync()
		{
			if (_context.Mwatches != null)
			{
				DateTime eDay = DateTime.Now;
				DateTime sDay = eDay.AddDays(-1);
				var watchs = await _context.Mwatches.AsNoTracking()
						.Where(w => w.WdateTime >= sDay && w.WdateTime <= eDay)
						.Select(w => new { w.Status, w.WdateTime, w.Org, w.Url })
						.OrderByDescending(w => w.WdateTime)
						.ToArrayAsync();
				return Content(JsonConvert.SerializeObject(watchs), "application/json");

			}
			return Content("[{\"Status\": \"ΩÒ»’üoŸY¡œ\",\"Wtime\": \"\"}]");
		}

		//public void OnGet()
		//      {
		//      }
	}
}
