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
    public class ChildcareManagerTests
    {

        IChildcareAccessor _childcareAccessor;


        [TestInitialize]
        public void TestSetup()
        {
            _childcareAccessor = new ChildcareFake();
            //_childcareAccessor = new Childcare();
        }

        /// <summary>
        /// Chase Martin
        /// Created: 2021/03/4
        /// 
        /// Tests that all childcare types are
        /// retrieved.
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllChildcareTypes()
        {
            // arrange
            const int expectedCount = 3;
            int actualCount;

            //act
            actualCount = _childcareAccessor.SelectAllChildcareTypes().Count;

            //assert
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
