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
using Microsoft.EntityFrameworkCore;
using RazorPagesLibrary.Model;
using RazorPagesWeb.Data;

namespace RazorPagesWeb.Pages.Ion
{
    public class EditModel : PageModel
    {
        private readonly RazorPagesWeb.Data.ApplicationDbContext _context;

        public EditModel(RazorPagesWeb.Data.ApplicationDbContext context)
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

            var ion =  await _context.Ions.FirstOrDefaultAsync(m => m.Id == id);
            if (ion == null)
            {
                return NotFound();
            }
            Ion = ion;
            try
            {
                ContentString = Ion.Content.ToString("0.000E0");
            }catch
            {

            }

            return Page();
        }

        [BindProperty]
        [RegularExpression(@"^[0-9]([,\.][0-9]{1,3}([eE][-\+]?[0-9]{1,3})?)?$")]
        [DisplayName("Content [g/l]")]
        public string ContentString { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                Ion.Content = Double.Parse(ContentString, NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint);
            }
            catch
            {
                return Page();
            }

            _context.Attach(Ion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IonExists(Ion.Id))
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

        private bool IonExists(int id)
        {
            return _context.Ions.Any(e => e.Id == id);
        }
    }
}
