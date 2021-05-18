using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessFakes;
using DataAccessInterfaces;
using DomainModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicTests
{
    [TestClass]
    public class FinancialCounselingManagerTests
    {

        IFinancialCounselingAccessor _financialCounselingAccessor;


        [TestInitialize]
        public void TestSetup()
        {
            _financialCounselingAccessor = new FinancialCounselingFake();
            //_financialCounselingAccessor = new FinancialCounseling();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/09
        /// 
        /// Tests that all counseling types are
        /// retrieved.
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllCounselingTypes()
        {
            // arrange
            const int expectedCount = 3;
            int actualCount;

            //act
            actualCount = _financialCounselingAccessor.SelectAllCounselingTypes().Count;

            //assert
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
