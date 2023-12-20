using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesLibrary.Model;
using RazorPagesWeb.Data;

namespace RazorPagesWeb.Pages.Pallet
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPagesWeb.Data.ApplicationDbContext _context;

        public DetailsModel(RazorPagesWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public RazorPagesLibrary.Model.Pallet Pallet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pallet = await _context.Pallets
                .Include(p => p.Water)
                .ThenInclude(w => w.Type)
                .Include(p => p.Water)
                .ThenInclude(w => w.Packaging)
                .Include(p => p.Delivery)
                .ThenInclude(d => d.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pallet == null)
            {
                return NotFound();
            }
            else
            {
                Pallet = pallet;
            }
            return Page();
        }
    }
}
