using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RazorPagesWeb.Pages.Stock
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesWeb.Data.ApplicationDbContext _context;

        public IndexModel(RazorPagesWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<RazorPagesLibrary.Model.Pallet> Pallets { get; set; } = default!;
        public IList<RazorPagesLibrary.Model.SaleUnit> SaleUnits { get; set; } = default!;

        [BindProperty]
        public IList<KeyValuePair<RazorPagesLibrary.Model.Water, int>> WaterStock { get; set; } = new List<KeyValuePair<RazorPagesLibrary.Model.Water, int>>();

        public async Task<IActionResult> OnGet()
        {
            var waters = await _context.Waters
                .Include(w => w.Type)
                .Include(w => w.Packaging)
                .ToListAsync();
            Pallets = await _context.Pallets
                .Include(p => p.Delivery)
                .Include(p => p.Water)
                .ToListAsync();
            SaleUnits = await _context.SaleUnits
                .Include(p => p.Sale)
                .Include(p => p.Water)
                .ToListAsync();

            foreach(var w in waters)
            {
                var fromPallets = Pallets.Where(p => p.WaterId == w.Id)
                    .Aggregate(0, (acc, pal) => acc + pal.Count * pal.Delivery.ItemsPerPallet);
                var sold = SaleUnits.Where(p => p.WaterId == w.Id)
                    .Aggregate(0, (acc, u) => acc + u.Count);
                WaterStock.Add(new KeyValuePair<RazorPagesLibrary.Model.Water, int>(w, fromPallets - sold));
            }

            return Page();
        }
    }
}
