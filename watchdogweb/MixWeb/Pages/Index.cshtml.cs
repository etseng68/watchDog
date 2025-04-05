using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace MixWeb.Pages
{
	[Authorize]
	public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        
         
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
			//var claims = HttpContext.User.Claims.ToList();
			//ViewData["UserName"] = claims.Where(m => m.Type == ClaimTypes.Name).First().Value;
			//ViewData["Role"] = claims.Where(m => m.Type == ClaimTypes.Role).First().Value;
			//ClaimsPrincipal user = HttpContext.User;
			//ClaimsPrincipal user = this.User;
			ViewData["user"] = this.User;
            return Page();
		}
    }
}