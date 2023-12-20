using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesLibrary.Model;
using RazorPagesWeb.Data;

namespace RazorPagesWeb.Pages.Packaging
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
            return Page();
        }

        [BindProperty]
        public RazorPagesLibrary.Model.Packaging Packaging { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            _context.Packagings.Add(Packaging);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
