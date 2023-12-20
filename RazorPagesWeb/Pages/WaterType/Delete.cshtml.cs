using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesWeb.Data;

namespace RazorPagesWeb.Pages.WaterType
{
    public class DeleteModel : PageModel
    {
        private readonly RazorPagesWeb.Data.ApplicationDbContext _context;

        public DeleteModel(RazorPagesWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RazorPagesLibrary.Model.WaterType WaterType { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watertype = await _context.WaterTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (watertype == null)
            {
                return NotFound();
            }
            else
            {
                WaterType = watertype;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watertype = await _context.WaterTypes.FindAsync(id);
            if (watertype != null)
            {
                WaterType = watertype;
                _context.WaterTypes.Remove(WaterType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
