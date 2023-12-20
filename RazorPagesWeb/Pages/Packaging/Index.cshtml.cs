using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesWeb.Data;

namespace RazorPagesWeb.Pages.Packaging
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesWeb.Data.ApplicationDbContext _context;

        public IndexModel(RazorPagesWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<RazorPagesLibrary.Model.Packaging> Packaging { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Packaging = await _context.Packagings.ToListAsync();
        }
    }
}
