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
    public class CreateModel : PageModel
    {
        private readonly RazorPagesWeb.Data.ApplicationDbContext _context;

        public CreateModel(RazorPagesWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["DeliveryId"] = new SelectList(_context.Deliveries.Include(d => d.Supplier), "Id", "DisplayName");
            ViewData["WaterId"] = new SelectList(_context.Waters.Include(w => w.Type).Include(w => w.Packaging), "Id", "DisplayName");
            return Page();
        }

        [BindProperty]
        public RazorPagesLibrary.Model.Pallet Pallet { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            _context.Pallets.Add(Pallet);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
