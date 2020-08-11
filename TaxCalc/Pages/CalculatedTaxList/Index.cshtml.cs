using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaxCalc.Models;

namespace TaxCalc.Pages.CalculatedTaxList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IndexModel( ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<PayableTax> CalculatedTaxes { get; set; }
        public async Task OnGet()
        {
            CalculatedTaxes = await _db.CalculatedTax.ToListAsync();
        }

        //Method to delete the calculated tax record 
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var calculatedTaxRecord = await _db.CalculatedTax.FindAsync(id);
            if(calculatedTaxRecord == null)
            {
                return NotFound();
            }
            _db.CalculatedTax.Remove(calculatedTaxRecord);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
