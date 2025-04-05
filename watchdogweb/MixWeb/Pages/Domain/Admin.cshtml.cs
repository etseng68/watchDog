using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace MixWeb.Pages.Domain
{
    [Authorize(Roles = "1")]
    public class AdminModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
