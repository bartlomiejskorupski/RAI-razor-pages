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
    public class IndexModel : PageModel
    {
        private readonly RazorPagesWeb.Data.ApplicationDbContext _context;

        public IndexModel(RazorPagesWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<RazorPagesLibrary.Model.SaleUnit> SaleUnit { get;set; } = default!;

        public async Task OnGetAsync()
        {
            SaleUnit = await _context.SaleUnits
                .Include(s => s.Sale)
                .Include(s => s.Water)
                .ThenInclude(w => w.Type)
                .Include(s => s.Water)
                .ThenInclude(w => w.Packaging)
                .ToListAsync();
        }
    }
}
