using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesLibrary.Model;
using RazorPagesWeb.Data;

namespace RazorPagesWeb.Pages.Ion
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
        public RazorPagesLibrary.Model.Ion Ion { get; set; } = default!;

        [BindProperty]
        [RegularExpression(@"^[0-9]([,\.][0-9]{1,3}([eE][-\+]?[0-9]{1,3})?)?$")]
        [DisplayName("Content [g/l]")]
        public string ContentString { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                Ion.Content = Double.Parse(ContentString, NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
            }
            catch
            {
                return Page();
            }

            _context.Ions.Add(Ion);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
