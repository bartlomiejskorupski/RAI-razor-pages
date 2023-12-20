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

namespace RazorPagesWeb.Pages.Delivery
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesWeb.Data.ApplicationDbContext _context;

        public CreateModel(RazorPagesWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            var waters = await _context.Waters.Include(w => w.Type).Include(w => w.Packaging).ToListAsync();
            ViewData["SupplierID"] = new SelectList(_context.Companies, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public RazorPagesLibrary.Model.Delivery Delivery { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            Delivery.Pallets = new List<RazorPagesLibrary.Model.Pallet>();
            
            _context.Deliveries.Add(Delivery);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
