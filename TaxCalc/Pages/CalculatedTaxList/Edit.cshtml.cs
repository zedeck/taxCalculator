using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaxCalc.Compute;
using TaxCalc.Models;

namespace TaxCalc.Pages.CalculatedTaxList
{
    public class EditModel : PageModel
    {
        private ApplicationDbContext _db;

        public EditModel( ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public PayableTax CalculatedTax { get; set; }

        public async Task OnGet(int id)
        {
            CalculatedTax = await _db.CalculatedTax.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {

                TaxComputation taxComputation = new TaxComputation();
                var TaxValueFrromDb = await _db.CalculatedTax.FindAsync(CalculatedTax.Id);
                var payableTax = taxComputation.payable(CalculatedTax);

                TaxValueFrromDb.PostalCode = CalculatedTax.PostalCode;
                TaxValueFrromDb.AnnualIncome = CalculatedTax.AnnualIncome;
                TaxValueFrromDb.CalcValue = payableTax.CalcValue;
                TaxValueFrromDb.TransDate = payableTax.TransDate;
                
                await _db.SaveChangesAsync();
                
                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}
