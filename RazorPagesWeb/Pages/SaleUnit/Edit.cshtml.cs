using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesLibrary.Model;
using RazorPagesWeb.Data;

namespace RazorPagesWeb.Pages.SaleUnit
{
    public class EditModel : PageModel
    {
        private readonly RazorPagesWeb.Data.ApplicationDbContext _context;

        public EditModel(RazorPagesWeb.Data.ApplicationDbContext context)
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

            var saleunit =  await _context.SaleUnits.FirstOrDefaultAsync(m => m.Id == id);
            if (saleunit == null)
            {
                return NotFound();
            }
            SaleUnit = saleunit;
            ViewData["SaleId"] = new SelectList(_context.Sales, "Id", "DisplayName");
            ViewData["WaterId"] = new SelectList(_context.Waters.Include(w => w.Type).Include(w => w.Packaging), "Id", "DisplayName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            _context.Attach(SaleUnit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleUnitExists(SaleUnit.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SaleUnitExists(int id)
        {
            return _context.SaleUnits.Any(e => e.Id == id);
        }
    }
}
