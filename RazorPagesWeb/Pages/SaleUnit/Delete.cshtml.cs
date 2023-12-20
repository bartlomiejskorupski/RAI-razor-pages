using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesLibrary.Model;
using RazorPagesWeb.Data;

namespace RazorPagesWeb.Pages.SaleUnit
{
    public class DeleteModel : PageModel
    {
        private readonly RazorPagesWeb.Data.ApplicationDbContext _context;

        public DeleteModel(RazorPagesWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RazorPagesLibrary.Model.SaleUnit SaleUnit { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleunit = await _context.SaleUnits
                .Include(su => su.Sale)
                .Include(su => su.Water)
                .ThenInclude(w => w.Type)
                .Include(su => su.Water)
                .ThenInclude(w => w.Packaging)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (saleunit == null)
            {
                return NotFound();
            }
            else
            {
                SaleUnit = saleunit;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleunit = await _context.SaleUnits.FindAsync(id);
            if (saleunit != null)
            {
                SaleUnit = saleunit;
                _context.SaleUnits.Remove(SaleUnit);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
