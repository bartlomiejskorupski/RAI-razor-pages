using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using RazorPagesLibrary.Model;
using RazorPagesWeb.Data;

namespace RazorPagesWeb.Pages.Water
{
    public class EditModel : PageModel
    {
        private readonly RazorPagesWeb.Data.ApplicationDbContext _context;

        public EditModel(RazorPagesWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RazorPagesLibrary.Model.Water Water { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var water =  await _context.Waters.Include(w => w.Ions).FirstOrDefaultAsync(m => m.Id == id);
            if (water == null)
            {
                return NotFound();
            }
            Water = water;

            IonIds = await _context.Ions.Select(i => i.Id).ToListAsync();

            ViewData["ManufacturerID"] = new SelectList(_context.Companies, "Id", "Name");
            ViewData["PackagingID"] = new SelectList(_context.Packagings, "Id", "DisplayName");
            ViewData["TypeID"] = new SelectList(_context.WaterTypes, "Id", "Name");
            ViewData["Ions"] = new SelectList(_context.Ions, "Id", "DisplayName");

            return Page();
        }

        [BindProperty]
        public ICollection<int> IonIds { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var water = _context.Waters
                .Include(w => w.Ions)
                .Single(w => w.Id == Water.Id);

            if (water == null)
                return Page();

            var newIons = await _context.Ions.Where(i => IonIds.Contains(i.Id)).ToListAsync();

            water.Ions.Clear();

            foreach(var ion in newIons)
            {
                water.Ions.Add(ion);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WaterExists(Water.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool WaterExists(int id)
        {
            return _context.Waters.Any(e => e.Id == id);
        }
    }
}
