using DataAccessFakes;
using DataAccessInterfaces;
using DomainModels;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicTests
{
    /// <summary>
    /// Richard Schroeder
    /// Created: 2021/03/05
    /// 
    /// SupplyItem test class to verify functionality of all public methods 
    /// for SupplyItem data objects
    /// </summary>
    [TestClass]
    public class SupplyInventoryManagerTests
    {
        private ISupplyInventoryAccessor _supplyInventoryAccessor;

        [TestInitialize]
        public void TestSetup()
        {
            _supplyInventoryAccessor = new SupplyItemFake();
        }


        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/10
        /// 
        /// Test to add a supply object to the SupplyInventory
        /// </summary>
        [TestMethod]
        public void TestAddSupplyItem()
        {
            // Arrange
            SupplyInventoryManager supplyInventoryManager = new SupplyInventoryManager(_supplyInventoryAccessor);
            const bool expected = true;

            SupplyItem testItem = new SupplyItem(){
                SupplyItemID = 100004,
                SupplySerialNumber = 852741963,
                MaterialName = "Packing Peanuts",
                SupplyDescription = "Tape for packing boxes",
                SupplyInventoryQuantity = 500
            };

            // Act
            bool actual = supplyInventoryManager.AddSupplyItem(testItem);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/12
        /// 
        /// Test to get the list of items from fakes
        /// </summary>
        [TestMethod]
        public void TestGetSupplyListCount()
        {
            //arrange
            SupplyInventoryManager supplyInventoryManager = new SupplyInventoryManager(_supplyInventoryAccessor);
            int expected = 3;

            //act
            int actual = supplyInventoryManager.ShowSupplyInventory().Count;

            //assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/12
        /// 
        /// Test to edit existing supply item information
        /// </summary>
        [TestMethod]
        public void TestEditSupplyItemGood()
        {
            // arrange
            SupplyInventoryManager supplyInventoryManager = new SupplyInventoryManager(_supplyInventoryAccessor);
            bool expected = true;
            SupplyItem testItem = new SupplyItem()
            {
                SupplyItemID = 100000, // same ID as fake
                SupplySerialNumber = 963147,
                MaterialName = "Test item",
                SupplyDescription = "This is for a test",
                SupplyInventoryQuantity = 1
            };

            SupplyItem fakeItemCopy = new SupplyItem()
            {
                SupplyItemID = 100000,
                SupplySerialNumber = 123456,
                MaterialName = "Packing Tape",
                SupplyDescription = "Tape for packing boxes",
                SupplyInventoryQuantity = 200
            };

            // act
            bool actual = supplyInventoryManager.EditSupplyItem(fakeItemCopy, testItem);

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/12
        /// 
        /// Test to catch exception to edit object with bad ID
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestEditSupplyItemBadID()
        {
            // arrange
            SupplyInventoryManager supplyInventoryManager = new SupplyInventoryManager(_supplyInventoryAccessor);
            bool expected = true;
            SupplyItem testItem = new SupplyItem()
            {
                SupplyItemID = 5, // bad item ID, less than 100000
                SupplySerialNumber = 741963,
                MaterialName = "Test item",
                SupplyDescription = "This is for a test",
                SupplyInventoryQuantity = 1
            };

            SupplyItem fakeItemCopy = new SupplyItem()
            {
                SupplyItemID = 100000,
                SupplySerialNumber = 123456,
                MaterialName = "Packing Tape",
                SupplyDescription = "Tape for packing boxes",
                SupplyInventoryQuantity = 200
            };

            // act
            bool actual = supplyInventoryManager.EditSupplyItem(fakeItemCopy, testItem);

            // assert
            // expect ApplicationException
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/12
        /// 
        /// Test to catch exception to edit object with bad serial number
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestEditSupplyItemBadSerialNumberLessThan100000()
        {
            // arrange
            SupplyInventoryManager supplyInventoryManager = new SupplyInventoryManager(_supplyInventoryAccessor);
            bool expected = true;
            SupplyItem testItem = new SupplyItem()
            {
                SupplyItemID = 100000, 
                SupplySerialNumber = 5, // bad serial number, less than 100000
                MaterialName = "Test item",
                SupplyDescription = "This is for a test",
                SupplyInventoryQuantity = 1
            };

            SupplyItem fakeItemCopy = new SupplyItem()
            {
                SupplyItemID = 100000,
                SupplySerialNumber = 123456,
                MaterialName = "Packing Tape",
                SupplyDescription = "Tape for packing boxes",
                SupplyInventoryQuantity = 200
            };

            // act
            bool actual = supplyInventoryManager.EditSupplyItem(fakeItemCopy, testItem);

            // assert
            // expect ApplicationException
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/12
        /// 
        /// Test to catch exception to edit object with bad serial number
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestEditSupplyItemBadSerialNumberGreaterThan999999()
        {
            // arrange
            SupplyInventoryManager supplyInventoryManager = new SupplyInventoryManager(_supplyInventoryAccessor);
            bool expected = true;
            SupplyItem testItem = new SupplyItem()
            {
                SupplyItemID = 100000,
                SupplySerialNumber = 1000000, // bad serial number, less than 100000
                MaterialName = "Test item",
                SupplyDescription = "This is for a test",
                SupplyInventoryQuantity = 1
            };

            SupplyItem fakeItemCopy = new SupplyItem()
            {
                SupplyItemID = 100000,
                SupplySerialNumber = 123456,
                MaterialName = "Packing Tape",
                SupplyDescription = "Tape for packing boxes",
                SupplyInventoryQuantity = 200
            };

            // act
            bool actual = supplyInventoryManager.EditSupplyItem(fakeItemCopy, testItem);

            // assert
            // expect ApplicationException
        }
    }
}
