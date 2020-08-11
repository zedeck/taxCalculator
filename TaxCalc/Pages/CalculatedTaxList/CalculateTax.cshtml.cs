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
    public class CalculateTaxModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public CalculateTaxModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public PayableTax CalculatedTax { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                //Call Calculations 
                TaxComputation computeTax = new TaxComputation();
                await _db.CalculatedTax.AddAsync(computeTax.payable(CalculatedTax));
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
               
            }
            else
            {
                return Page();
            }
        }
    }
}
