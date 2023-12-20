using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesLibrary.Model;
using RazorPagesWeb.Data;

namespace RazorPagesWeb.Pages.Ion
{
    public class DeleteModel : PageModel
    {
        private readonly RazorPagesWeb.Data.ApplicationDbContext _context;

        public DeleteModel(RazorPagesWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RazorPagesLibrary.Model.Ion Ion { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ion = await _context.Ions.FirstOrDefaultAsync(m => m.Id == id);

            if (ion == null)
            {
                return NotFound();
            }
            else
            {
                Ion = ion;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ion = await _context.Ions.FindAsync(id);
            if (ion != null)
            {
                Ion = ion;
                _context.Ions.Remove(Ion);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
