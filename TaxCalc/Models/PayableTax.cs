using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalc.Models
{
    public class PayableTax
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double AnnualIncome { get; set; }
        [Required]
        public string  PostalCode { get; set; }

        public DateTime TransDate { get; set; }
        public double CalcValue { get; set; } 


    }
}
