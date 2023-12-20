using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesLibrary.Model;
using RazorPagesWeb.Data;

namespace RazorPagesWeb.Pages.Packaging
{
    public class DeleteModel : PageModel
    {
        private readonly RazorPagesWeb.Data.ApplicationDbContext _context;

        public DeleteModel(RazorPagesWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RazorPagesLibrary.Model.Packaging Packaging { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packaging = await _context.Packagings.FirstOrDefaultAsync(m => m.Id == id);

            if (packaging == null)
            {
                return NotFound();
            }
            else
            {
                Packaging = packaging;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packaging = await _context.Packagings.FindAsync(id);
            if (packaging != null)
            {
                Packaging = packaging;
                _context.Packagings.Remove(Packaging);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
