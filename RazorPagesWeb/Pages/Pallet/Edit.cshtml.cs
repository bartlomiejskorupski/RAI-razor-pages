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

namespace RazorPagesWeb.Pages.Pallet
{
    public class EditModel : PageModel
    {
        private readonly RazorPagesWeb.Data.ApplicationDbContext _context;

        public EditModel(RazorPagesWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RazorPagesLibrary.Model.Pallet Pallet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pallet =  await _context.Pallets.FirstOrDefaultAsync(m => m.Id == id);
            if (pallet == null)
            {
                return NotFound();
            }
            Pallet = pallet;
            ViewData["DeliveryId"] = new SelectList(_context.Deliveries.Include(d => d.Supplier), "Id", "DisplayName");
            ViewData["WaterId"] = new SelectList(_context.Waters.Include(w => w.Type).Include(w => w.Packaging), "Id", "DisplayName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            _context.Attach(Pallet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PalletExists(Pallet.Id))
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

        private bool PalletExists(int id)
        {
            return _context.Pallets.Any(e => e.Id == id);
        }
    }
}
