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
    public class IndexModel : PageModel
    {
        private readonly RazorPagesWeb.Data.ApplicationDbContext _context;

        public IndexModel(RazorPagesWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<RazorPagesLibrary.Model.Pallet> Pallet { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Pallet = await _context.Pallets
                .Include(p => p.Delivery)
                .ThenInclude(d => d.Supplier)
                .Include(p => p.Water)
                .ThenInclude(w => w.Type)
                .Include(p => p.Water)
                .ThenInclude(w => w.Packaging)
                .ToListAsync();
        }
    }
}
