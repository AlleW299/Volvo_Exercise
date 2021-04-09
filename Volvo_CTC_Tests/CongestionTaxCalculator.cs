using NUnit.Framework;
using System;
using System.Collections.Generic;
using Volvo_CTC.CommonFunctions;
using Volvo_CTC.Models;
using Volvo_CTC_Tests.Models;

namespace Volvo_CTC_Tests
{
    public class CongestionTaxCalculator_Tests
    {
        private List<DateTime> testDateTimes;
        private List<GetTaxTestData> getTaxScenarios = new List<GetTaxTestData>();
        private List<IsTollFreeDateTestData> IsTollFreeDateScenarios = new List<IsTollFreeDateTestData>();

        /// <summary>
        /// Would obviously move this to a seperat function if time permittet.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            testDateTimes = new List<DateTime> {
                Convert.ToDateTime("2013-01-14 21:00:00"),
                Convert.ToDateTime("2013-01-15 21:00:00"),
                Convert.ToDateTime("2013-02-07 06:23:27"),
                Convert.ToDateTime("2013-02-07 15:27:00"),
                Convert.ToDateTime("2013-02-08 06:27:00"),
                Convert.ToDateTime("2013-02-08 06:20:27"),
                Convert.ToDateTime("2013-02-08 14:35:00"),
                Convert.ToDateTime("2013-02-08 15:29:00"),
                Convert.ToDateTime("2013-02-08 15:47:00"),
                Convert.ToDateTime("2013-02-08 16:01:00"),
                Convert.ToDateTime("2013-02-08 16:48:00"),
                Convert.ToDateTime("2013-02-08 17:49:00"),
                Convert.ToDateTime("2013-02-08 18:29:00"),
                Convert.ToDateTime("2013-02-08 18:35:00"),
                Convert.ToDateTime("2013-03-28 14:07:27") };

            getTaxScenarios.Add(new GetTaxTestData { Dates = new DateTime[] {   testDateTimes[0] }, Result = new ExpectedResult { Car = 0, MotorBike = 0 } });
            getTaxScenarios.Add(new GetTaxTestData { Dates = new DateTime[] {   testDateTimes[1] }, Result = new ExpectedResult { Car = 0, MotorBike = 0 } });
            getTaxScenarios.Add(new GetTaxTestData { Dates = new DateTime[] {   testDateTimes[2],
                                                                                testDateTimes[3] }, Result = new ExpectedResult { Car = 13, MotorBike = 0 } });
            getTaxScenarios.Add(new GetTaxTestData { Dates = new DateTime[] {   testDateTimes[4],
                                                                                testDateTimes[5],
                                                                                testDateTimes[6],
                                                                                testDateTimes[7],
                                                                                testDateTimes[8],
                                                                                testDateTimes[9],
                                                                                testDateTimes[10],
                                                                                testDateTimes[11],
                                                                                testDateTimes[12],
                                                                                testDateTimes[13] }, Result = new ExpectedResult { Car = 48, MotorBike = 0 } });
            getTaxScenarios.Add(new GetTaxTestData { Dates = new DateTime[] {   testDateTimes[14] }, Result = new ExpectedResult { Car = 0, MotorBike = 0 } });

            IsTollFreeDateScenarios.Add(new IsTollFreeDateTestData { Date = testDateTimes[0], TollFree = false });
            IsTollFreeDateScenarios.Add(new IsTollFreeDateTestData { Date = testDateTimes[1], TollFree = false });
            IsTollFreeDateScenarios.Add(new IsTollFreeDateTestData { Date = testDateTimes[2], TollFree = false });
            IsTollFreeDateScenarios.Add(new IsTollFreeDateTestData { Date = testDateTimes[3], TollFree = false });
            IsTollFreeDateScenarios.Add(new IsTollFreeDateTestData { Date = testDateTimes[4], TollFree = false });
            IsTollFreeDateScenarios.Add(new IsTollFreeDateTestData { Date = testDateTimes[5], TollFree = false });
            IsTollFreeDateScenarios.Add(new IsTollFreeDateTestData { Date = testDateTimes[6], TollFree = false });
            IsTollFreeDateScenarios.Add(new IsTollFreeDateTestData { Date = testDateTimes[7], TollFree = false });
            IsTollFreeDateScenarios.Add(new IsTollFreeDateTestData { Date = testDateTimes[8], TollFree = false });
            IsTollFreeDateScenarios.Add(new IsTollFreeDateTestData { Date = testDateTimes[9], TollFree = false });
            IsTollFreeDateScenarios.Add(new IsTollFreeDateTestData { Date = testDateTimes[10], TollFree = false });
            IsTollFreeDateScenarios.Add(new IsTollFreeDateTestData { Date = testDateTimes[11], TollFree = false });
            IsTollFreeDateScenarios.Add(new IsTollFreeDateTestData { Date = testDateTimes[12], TollFree = false });
            IsTollFreeDateScenarios.Add(new IsTollFreeDateTestData { Date = testDateTimes[13], TollFree = false });
            IsTollFreeDateScenarios.Add(new IsTollFreeDateTestData { Date = testDateTimes[14], TollFree = true });
        }

        [Test]
        public void GetTax_MotorBike()
        {
            var testNr = 0;
            foreach (var test in getTaxScenarios)
            {
                testNr++;
                int testResult = new CongestionTaxCalculator().GetTax(new Motorcycle(), test.Dates);
                Assert.AreEqual(testResult, test.Result.MotorBike, string.Format("Test {0} failed.", testNr));
            }
        }

        [Test]
        public void GetTax_Car()
        {
            var testNr = 0;
            foreach (var test in getTaxScenarios)
            {
                testNr++;
                int testResult = new CongestionTaxCalculator().GetTax(new Car(), test.Dates);
                Assert.AreEqual(testResult, test.Result.Car, string.Format("Test {0} failed.", testNr));
            }
        }

        [Test]
        public void IsTollFreeDate()
        {
            var testNr = 0;
            foreach (var test in IsTollFreeDateScenarios)
            {
                testNr++;
                bool testResult = new CongestionTaxCalculator().IsTollFreeDate(test.Date);
                Assert.AreEqual(testResult, test.TollFree, string.Format("Test {0} failed.", testNr));
            }
        }
        [Test]
        public void IsTollFreeVehicle()
        {
            bool testResult = new CongestionTaxCalculator().IsTollFreeVehicle(new Car());
            Assert.AreEqual(testResult, false, "Car Test failed.");

            testResult = new CongestionTaxCalculator().IsTollFreeVehicle(new Motorcycle());
            Assert.AreEqual(testResult, true, "Motorcycle Test failed.");
        }


        
    }
}

