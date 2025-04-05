using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MixWeb.Models;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace MixWeb.Pages.Authenticate
{
	public class LoginModel : PageModel
    {
		private readonly IConfiguration _configuration;
		private readonly MixWebContext _context;

		[BindProperty]
		public InputModel Input { get; set; }
		public class InputModel
		{
			[Required]
			public string? UserName { get; set; }

			[Required]
			[DataType(DataType.Password)]
			public string? Password { get; set; }
		}

		public LoginModel(IConfiguration configuration, MixWebContext context)

		{
			_configuration = configuration;
			_context = context;
		}

        public IActionResult OnGet()
        {
			return Page();
		}

		public Maccount maccount { get; set; }
		public MaccessLog maccountLog { get; set; }
    public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				Maccount? maccount = await _context.Maccounts.FirstOrDefaultAsync(m => m.Name == Input.UserName && m.Password == Input.Password);
				if (maccount == null)
				{
					ModelState.AddModelError(string.Empty, "Invalid username or password.");
				}
				else
				{
					
                     //string[] redirects = new string[] { "/index", "/index", "/admin" };
                     //string[] roles = new string[] { "User", "OrgAdmin", "WebAdmin" };
                     //var claims = new[]
                     //{
                     //    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, Input.UserName)
                     //};
                     var claims = new List<Claim>
					{
						new Claim("UserId", maccount.Id.ToString()),
						new Claim(ClaimTypes.Name, maccount.Name),
						new Claim(ClaimTypes.Role, maccount.Role.ToString())
					};
					
					//Ö^”Úcookie Ö¢îµ
					var authProperties = new AuthenticationProperties
					{
						//AllowRefresh = <bool>,
						// Refreshing the authentication session should be allowed.

						//ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
						// The time at which the authentication ticket expires. A 
						// value set here overrides the ExpireTimeSpan option of 
						// CookieAuthenticationOptions set with AddCookie.

						//IsPersistent = true,
						// Whether the authentication session is persisted across 
						// multiple requests. When used with cookies, controls
						// whether the cookie's lifetime is absolute (matching the
						// lifetime of the authentication ticket) or session-based.

						//IssuedUtc = <DateTimeOffset>,
						// The time at which the authentication ticket was issued.

						//RedirectUri = <string>
						// The full path or absolute URI to be used as an http 
						// redirect response value.
					};
					var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
					var principal = new ClaimsPrincipal(identity);
                    

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,authProperties);

      	//			String? cookieStr=null;
       //             if (identity != null)
       //             {
       //                 var cookieClaim = identity.Claims.FirstOrDefault(c => c.Type.StartsWith("Cookie"));
       //                 if (cookieClaim != null)
       //                 {
       //                     var cookieName = cookieClaim.Type.Replace("Cookie.", "");
       //                     var cookieValue = cookieClaim.Value;
							//cookieStr =cookieValue.ToString()+":"+cookieValue.ToString();
       //                     // do something with cookieName and cookieValue
       //                 }
       //             }
                    maccountLog = new MaccessLog();
                    maccountLog.UserId = maccount.Id;
                    maccountLog.UserIp = HttpContext.Connection.RemoteIpAddress.ToString();
					maccountLog.Note = "µ«»Î";
					_context.MaccessLogs.Add(maccountLog);
					await _context.SaveChangesAsync();

                    return RedirectToPage("/index");
					//return RedirectToPage(redirects[Wuser.Role]);
				}

				//if (ValidateUser(Input.UserName, Input.Password))
				//{
				//    var claims = new[]
				//    {
				//    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, Input.UserName)
				//    };

				//    var identity = new System.Security.Claims.ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				//    var principal = new System.Security.Claims.ClaimsPrincipal(identity);

				//    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
				//    return RedirectToPage("/Authenticated");
				//}
				//else
				//{
				//    ModelState.AddModelError(string.Empty, "Invalid username or password.");
				//}
			}

			return Page();
		}

		//private bool ValidateUser(string userName, string password)
		//{
		//	bool isValid = false;

		//	string connectionString = _configuration.GetConnectionString("DefaultConnection");

		//	using (SqlConnection connection = new SqlConnection(connectionString))
		//	{
		//		string query = "SELECT * FROM Users WHERE UserName = @UserName AND Password = @Password";

		//		SqlCommand command = new SqlCommand(query, connection);
		//		command.Parameters.AddWithValue("@UserName", userName);
		//		command.Parameters.AddWithValue("@Password", password);

		//		connection.Open();
		//		SqlDataReader reader = command.ExecuteReader();

		//		if (reader.HasRows)
		//		{
		//			isValid = true;
		//		}

		//		reader.Close();
		//	}

		//	return isValid;
		//}
	}
}
