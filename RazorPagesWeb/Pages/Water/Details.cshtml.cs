using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesLibrary.Model;
using RazorPagesWeb.Data;

namespace RazorPagesWeb.Pages.Water
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPagesWeb.Data.ApplicationDbContext _context;

        public DetailsModel(RazorPagesWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RazorPagesLibrary.Model.Water Water { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var water = await _context.Waters
                .Include(w => w.Type)
                .Include(w => w.Manufacturer)
                .Include(w => w.Ions)
                .Include(w => w.Packaging)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (water == null)
            {
                return NotFound();
            }
            else
            {
                Water = water;
            }
            return Page();
        }
    }
}
