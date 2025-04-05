using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MixWeb.Models;
using System.Security.Principal;

namespace MixWeb.Pages.Authenticate
{
    public class LogoutModel : PageModel
    {
        private readonly MixWebContext _context;
        public LogoutModel(MixWebContext context)
        {
            _context = context;
        }

        public MaccessLog maccountLog { get; set; }
        public async Task<RedirectResult> OnGet()
        {
			await HttpContext.SignOutAsync();
            maccountLog = new MaccessLog();
            maccountLog.UserId = int.Parse(this.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            maccountLog.UserIp = HttpContext.Connection.RemoteIpAddress.ToString();
            maccountLog.Note = "µÇ³ö";
            _context.MaccessLogs.Add(maccountLog);
            await _context.SaveChangesAsync();
			return Redirect("/index");
		}
		public async Task<IActionResult> OnPost()
		{
			await HttpContext.SignOutAsync();
			return Redirect("login");
			//return RedirectToPage("/index");
		}

	}
}
