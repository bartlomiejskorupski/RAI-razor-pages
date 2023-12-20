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

namespace RazorPagesWeb.Pages.Water
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
            IonIds = await _context.Ions.Select(i => i.Id).ToListAsync();

            ViewData["ManufacturerID"] = new SelectList(_context.Companies, "Id", "Name");
            ViewData["PackagingID"] = new SelectList(_context.Packagings, "Id", "DisplayName");
            ViewData["TypeID"] = new SelectList(_context.WaterTypes, "Id", "Name");
            ViewData["Ions"] = new SelectList(_context.Ions, "Id", "DisplayName");

            return Page();
        }

        [BindProperty]
        public RazorPagesLibrary.Model.Water Water { get; set; } = default!;

        [BindProperty]
        public ICollection<int> IonIds { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            Water.Ions = await _context.Ions.Where(i => IonIds.Contains(i.Id)).ToListAsync();

            _context.Waters.Add(Water);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
