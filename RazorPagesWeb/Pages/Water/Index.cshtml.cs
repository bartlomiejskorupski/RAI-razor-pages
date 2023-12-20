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
    public class IndexModel : PageModel
    {
        private readonly RazorPagesWeb.Data.ApplicationDbContext _context;

        public IndexModel(RazorPagesWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<RazorPagesLibrary.Model.Water> Water { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Water = await _context.Waters
                .Include(w => w.Manufacturer)
                .Include(w => w.Packaging)
                .Include(w => w.Type)
                .Include(w => w.Ions)
                .ToListAsync();
        }
    }
}
