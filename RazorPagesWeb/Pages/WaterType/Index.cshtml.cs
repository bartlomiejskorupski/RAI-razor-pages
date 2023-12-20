using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesWeb.Data;
using RazorPagesLibrary.Model;

namespace RazorPagesWeb.Pages.WaterType
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<RazorPagesLibrary.Model.WaterType> WaterType { get;set; } = default!;

        public async Task OnGetAsync()
        {
            WaterType = await _context.WaterTypes.ToListAsync();
        }
    }
}
