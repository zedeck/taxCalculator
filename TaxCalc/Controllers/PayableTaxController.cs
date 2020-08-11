using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxCalc.Compute;
using TaxCalc.Models;

namespace TaxCalc.Controllers
{
    [Route("api/PayableTax")]
    [ApiController]
    public class PayableTaxController : Controller
    {
        private readonly ApplicationDbContext _db;
        //public PayableTax CalculatedTax { get; set; }

        public PayableTaxController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.CalculatedTax.ToListAsync() });
        }

        //Delete Controller
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var calculatedTaxRecordFromDb = await _db.CalculatedTax.FirstOrDefaultAsync(u => u.Id == id);
            if(calculatedTaxRecordFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _db.CalculatedTax.Remove(calculatedTaxRecordFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete Successful" });
        }
        
    }
}
