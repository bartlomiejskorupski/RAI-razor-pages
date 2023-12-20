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
    public class DeleteModel : PageModel
    {
        private readonly RazorPagesWeb.Data.ApplicationDbContext _context;

        public DeleteModel(RazorPagesWeb.Data.ApplicationDbContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var water = await _context.Waters.FindAsync(id);
            if (water != null)
            {
                Water = water;
                _context.Waters.Remove(Water);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
