using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MixWeb.Models;
using Newtonsoft.Json;

namespace MixWeb.Pages.Domain
{
	[Authorize]
	public class ListModel : PageModel
	{
		private readonly MixWebContext _context;

		public ListModel(MixWebContext context)
		{
			_context = context;
		}
		//public IList<Mdomain> Mdomain { get; set; } = default!;
		public async Task<IActionResult> OnGetAsync()
		{
			if (_context.Mdomains != null)
			{
				string userName = User.FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
				Maccount maccount = await _context.Maccounts.AsNoTracking()
					.Where(a => a.Name == userName)
					.SingleOrDefaultAsync();

				
				
                object? domains = null;
				if (Request.Query.ContainsKey("domain"))
				{
					string domainName = Request.Query["domain"];
					if (domainName == "all")
					{
						if(maccount.Role == 2) { 
							domains = await _context.Mdomains.AsNoTracking()
									.Where(d => d.Del == false)
									.Where(d => d.Org == maccount.Org)
									.Select(d => new { d.Id, d.Wurl, d.Wrun, d.Org, d.CreateAt, d.Wmemo })
									.ToArrayAsync();
						}
						else
						{
                            domains = await _context.Mdomains.AsNoTracking()
                                    .Where(d => d.Del == false)            
                                    .Select(d => new { d.Id, d.Wurl, d.Wrun, d.Org, d.CreateAt, d.Wmemo })
                                    .ToArrayAsync();
                        }

					}
					else
					{
						if (maccount.Role == 2)
						{
							domains = await _context.Mdomains.AsNoTracking()
								.Where(d => d.Del == false)
								.Where(d => d.Org == maccount.Org)
								.Where(d => d.Wurl == domainName)
								.Select(d => new { d.Id, d.Wurl, d.Wrun, d.Org, d.CreateAt, d.Wmemo })
								.ToArrayAsync();
						}
						else
						{
                            domains = await _context.Mdomains.AsNoTracking()
                                .Where(d => d.Del == false)               
                                .Where(d => d.Wurl == domainName)
                                .Select(d => new { d.Id, d.Wurl, d.Wrun, d.Org, d.CreateAt, d.Wmemo })
                                .ToArrayAsync();
                        }
					}
				}
				else
				{
					if (maccount.Role == 2)
					{
						domains = await _context.Mdomains.AsNoTracking()
							.Where(d => d.Wrun == true)
							.Where(d => d.Del == false)
							.Where(d => d.Org == maccount.Org)
							.Select(d => new { d.Id, d.Wurl, d.Wrun, d.Org, d.CreateAt, d.Wmemo })
							.ToArrayAsync();
					}
					else
					{
                        domains = await _context.Mdomains.AsNoTracking()
							.Where(d => d.Wrun == true)
							.Where(d => d.Del == false)
							.Select(d => new { d.Id, d.Wurl, d.Wrun, d.Org, d.CreateAt, d.Wmemo })
							.ToArrayAsync();
                    }
					//return Content("{\"data\":" + JsonConvert.SerializeObject(domain) + "}", "application/json");
				}
				return Content(JsonConvert.SerializeObject(domains), "application/json");
			}
			return Content("[{\"Wurl\": \"üoŸY¡œ\"}]");
		}
	}
}
