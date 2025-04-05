using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MixWeb.Models;

namespace MixWeb.Pages.Watch
{
	[Authorize]
	public class AdminModel : PageModel
    {
        private readonly Models.MixWebContext _context;

		public AdminModel(Models.MixWebContext context)
		{
			_context = context;
		}
		public IList<Mwatch> Mwatch { get; set; } = default!;
		public async Task OnGetAsync()
		{
			if (_context.Mwatches != null)
			{
				Mwatch = await _context.Mwatches.ToListAsync();
			}

		}
	}
}
