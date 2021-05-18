using DataAccessFakes;
using DataAccessInterfaces;
using DomainModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicTests
{
    /// <summary>
    /// Thomas Stout
    /// Created: 2021/02/17
    /// 
    /// Class for testing InventoryManager
    /// logic methods.
    /// </summary>
    [TestClass]
    public class InventoryManagerTests
    {
        //Instantiates IInventoryAccessor
        IInventoryAccessor _inventoryAccessor;

        //Sets _inventoryAccessor to InventoryFake().
        [TestInitialize]
        public void TestSetup()
        {
            _inventoryAccessor = new InventoryFake();
        }
        
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/25
        /// 
        /// Tests that an item is inserted and the count
        /// is increased.
        /// </summary>
        [TestMethod]
        public void TestInsertInventoryItemReturnsRowsAffected()
        {
            Inventory inventory = new Inventory();
            // arrange
            const int expectedCount = 6;
            int actualCount;

            // act
            actualCount = _inventoryAccessor.InsertInventoryItem(inventory);


            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/25
        /// 
        /// Tests that all inventory items are
        /// retrieved.
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllInventoryItemsReturnsItems()
        {
            // arrange
            const int expectedCount = 5;
            int actualCount;

            //act
            actualCount = _inventoryAccessor.SelectAllInventoryItems().Count;

            //assert
            Assert.AreEqual(expectedCount, actualCount);
        }
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/25
        /// 
        /// Tests that an inventory item
        /// is deleted and the count is
        /// decreased.
        /// </summary>
        [TestMethod]
        public void TestDeleteInventoryItemRemovesItem()
        {
            Inventory inventory = new Inventory();
            // arrange
            const int expectedCount = 4;
            int actualCount;

            // act
            actualCount = _inventoryAccessor.DeleteInventoryItem(inventory.InventoryID);

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/25
        /// 
        /// Tests that an inventory item
        /// is edited and the count remains
        /// the same.
        /// </summary>
        [TestMethod]
        public void TestEditInventoryItemReturnsUpdatedItem()
        {
            Inventory inventory = new Inventory();
            // arrange
            const int expectedCount = 5;
            int actualCount;


            // act
            actualCount = _inventoryAccessor.UpdateInventoryItem(inventory);

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
