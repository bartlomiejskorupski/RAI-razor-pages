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

namespace RazorPagesWeb.Pages.Packaging
{
    public class EditModel : PageModel
    {
        private readonly RazorPagesWeb.Data.ApplicationDbContext _context;

        public EditModel(RazorPagesWeb.Data.ApplicationDbContext context)
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

            var packaging =  await _context.Packagings.FirstOrDefaultAsync(m => m.Id == id);
            if (packaging == null)
            {
                return NotFound();
            }
            Packaging = packaging;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _context.Attach(Packaging).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackagingExists(Packaging.Id))
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

        private bool PackagingExists(int id)
        {
            return _context.Packagings.Any(e => e.Id == id);
        }
    }
}
