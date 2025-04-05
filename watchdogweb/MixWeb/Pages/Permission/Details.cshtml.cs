using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MixWeb.Models;

namespace MixWeb.Pages.Permission
{
    [Authorize(Roles = "1")]
    public class DetailsModel : PageModel
    {
        private readonly MixWeb.Models.MixWebContext _context;

        public DetailsModel(MixWeb.Models.MixWebContext context)
        {
            _context = context;
        }

      public Mpermission Mpermission { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Mpermissions == null)
            {
                return NotFound();
            }

            var mpermission = await _context.Mpermissions.FirstOrDefaultAsync(m => m.Id == id);
            if (mpermission == null)
            {
                return NotFound();
            }
            else 
            {
                Mpermission = mpermission;
            }
            return Page();
        }
    }
}
