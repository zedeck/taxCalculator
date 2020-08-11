using NUnit.Framework;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using TaxCalc.Compute;
using TaxCalc.Models;

namespace TaxCalc.Test
{
    [TestFixture]
    public class TaxComputationTest
    {

        //Test type of Tax selection
        [Test]
        public void TestTypeOfTaxSelection()
        {
            var payableTax = new PayableTax();
            payableTax.AnnualIncome = 8000;
            payableTax.PostalCode = "1000";

            Tuple<string, string>[] typeOfTaxes = { new Tuple<string, string>("7441","Progressive"),
                                                        new Tuple<string, string>("A100","Flat Value"),
                                                        new Tuple<string, string>("7000","Flat Rate"),
                                                        new Tuple<string, string>("1000","Progressive") };

            var testPayableTax = new TaxComputation();
            string testResults = testPayableTax.getTypeOfTax(payableTax);
            string testComparer = "NoType";
            
            foreach(var typeOfTax in typeOfTaxes)
            {
                if (typeOfTax.Item1 == payableTax.PostalCode)
                    testComparer = typeOfTax.Item2;
            }
            Assert.AreEqual(testComparer, testResults);

        }

        //Test Type of Rate Computation
        [Test]
        public void TestTypeOfRateComputation()
        {
            Assert.Pass();
        }

        //Test FlatValue
        [Test]
        public void TestFlatValue()
        {
            Assert.Pass();
        }

        //Test Flat Rate
        [Test]
        public void TestFlatRate()
        {
            Assert.Pass();
        }


        //Test Aggressive Rate
        [Test]
        public void TestAggressiveRate()
        {
            var payableTax = new PayableTax();
            payableTax.AnnualIncome = 8000 ;
            payableTax.PostalCode = "7441";

            var testPayableTax = new TaxComputation();
            var testResults = testPayableTax.payable(payableTax);
            Assert.AreEqual(800, testResults.CalcValue);

        }
    }
}