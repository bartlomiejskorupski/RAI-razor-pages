using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesLibrary.Model;
using RazorPagesWeb.Data;

namespace RazorPagesWeb.Pages.Ion
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPagesWeb.Data.ApplicationDbContext _context;

        public DetailsModel(RazorPagesWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public RazorPagesLibrary.Model.Ion Ion { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ion = await _context.Ions.FirstOrDefaultAsync(m => m.Id == id);
            if (ion == null)
            {
                return NotFound();
            }
            else
            {
                Ion = ion;
            }
            return Page();
        }
    }
}
