using System;
using TaxCalc.Models;

namespace TaxCalc.Compute
{
    public class TaxComputation
    {
        private Tuple<string, string>[] typeOfTaxCalculation = { new Tuple<string, string>("7441","Progressive"),
                                                        new Tuple<string, string>("A100","Flat Value"),
                                                        new Tuple<string, string>("7000","Flat Rate"),
                                                        new Tuple<string, string>("1000","Progressive") };

        private Tuple<int, double, double>[] progressiveTaxTable = { new Tuple<int, double, double>(10, 0, 8350),
                                                                     new Tuple<int, double, double>(15, 8351,33950),
                                                                     new Tuple<int, double, double>(25, 33951, 82250),
                                                                     new Tuple<int, double, double>(28, 82251, 171550),
                                                                     new Tuple<int, double, double>(33, 171551, 372950),
                                                                     new Tuple<int, double, double>(35,372951,9999999999)};

        #region:calcPayableTax
        public PayableTax payable(PayableTax inputData)
        {

            PayableTax outputData = new PayableTax();
            //get type of tax calculation
            string typeOfTax = getTypeOfTax(inputData);
            
            switch (typeOfTax)
            {
                case "Progressive":
                    outputData.CalcValue = ProgressiveTaxCalc(inputData.AnnualIncome);
                    break;
                case "Flat Value":
                    outputData.CalcValue = FlatValueTaxCalc(inputData.AnnualIncome);
                    break;
                case "Flat Rate":
                    outputData.CalcValue = (inputData.AnnualIncome*17.5)/100;
                    break;
                case "NoType":
                    outputData.CalcValue = 0;
                    break;
            }

            outputData.AnnualIncome = inputData.AnnualIncome;
            outputData.Id = inputData.Id;
            outputData.TransDate = DateTime.Today;
            outputData.PostalCode = inputData.PostalCode;

            return outputData;
        }
        #endregion

        #region:getTypeOfTaxRegion
        public string getTypeOfTax(PayableTax inputData)
        {

            string typeOfTax = "NoType";
            //get type of tax calculation
            foreach (var taxCalculationTypes in typeOfTaxCalculation)
            {
                //If theres a hit
                if (taxCalculationTypes.Item1 == inputData.PostalCode)
                {
                    typeOfTax = taxCalculationTypes.Item2;
                }
            }
            return typeOfTax;
        }
        #endregion
    

        #region:getProgressiveTaxValue
        public double ProgressiveTaxCalc(double annualSalary)
        {
            double progressiveTaxValueAmount = 0;
            //get tax percentage per annual salary
            int prevTaxRate = 0;
            

            foreach (var progTaxBracketValues in progressiveTaxTable)
            {
                
                //If annual Salary Falls within a range
                if (annualSalary >= progTaxBracketValues.Item2 && annualSalary <= progTaxBracketValues.Item3)
                {
                    if (prevTaxRate > 0)   //If it is not the first Tax bracket
                    {

                        progressiveTaxValueAmount = (((progTaxBracketValues.Item2 * prevTaxRate )/100) + (((annualSalary - progTaxBracketValues.Item2)*progTaxBracketValues.Item1)/100));
                    }
                    else
                    {
                        progressiveTaxValueAmount = (annualSalary * progTaxBracketValues.Item1) / 100;
                    }
                }
                prevTaxRate = progTaxBracketValues.Item1;  //get prev_taxRate
                
            }

            return progressiveTaxValueAmount;
        }
        #endregion

        #region:getFlatValueTaxValue
        public double FlatValueTaxCalc(double annualSalary)
        {
            double flatValueTaxAmount = 0;
            if (annualSalary < 200000)
                flatValueTaxAmount = ((annualSalary * 5) / 100);
            else
                flatValueTaxAmount = 10000;

            return flatValueTaxAmount;
        }
        #endregion
    }
}
