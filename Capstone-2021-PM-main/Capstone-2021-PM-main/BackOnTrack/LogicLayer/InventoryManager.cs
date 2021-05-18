using DataAccessInterfaces;
using DataAccessLayer;
using DomainModels;
using LogicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Thomas Stout
    /// Created: 2021/02/17
    /// 
    /// Class for Inventory Management
    /// </summary>
    public class InventoryManager : IInventoryManager
    {
        private IInventoryAccessor _inventoryAccessor = null;

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/17
        /// 
        /// Empty constructor that holds the InventoryAccessor
        /// </summary>
        public InventoryManager()
        {
            _inventoryAccessor = new InventoryAccessor();
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/17
        /// 
        /// Dependency Inversion
        /// </summary>
        /// <param name="dataAccessor"></param>
        public InventoryManager(IInventoryAccessor dataAccessor) 
        {
            _inventoryAccessor = dataAccessor;
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/18
        /// 
        /// Calls the InsertInventoryItem() method from the DataAccessLayer
        /// to add an item to inventory
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        public bool AddInventoryItem(Inventory inventory)
        {
            bool result = false;
            try
            {
                result = (1 == _inventoryAccessor.InsertInventoryItem(inventory));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Item could not be added to inventory.", ex);
            }
            return result;
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/25
        /// 
        /// Calls the DeleteInventoryItem() method from the
        /// DataAccessLayer to delete an item from inventory.
        /// </summary>
        /// <param name="inventoryID"></param>
        /// <returns></returns>
        public bool DeleteInventoryItem(int inventoryID)
        {
            bool result = false;
            try
            {
                result = (1 == _inventoryAccessor.DeleteInventoryItem(inventoryID));
                
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Item could not be removed from inventory.", ex);
            }
            return result;
        }
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/25
        /// 
        /// Calls the UpdateInventoryItem() method from the
        /// DataAccessLayer to edit/update an inventory item.
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        public bool EditInventoryItem(Inventory inventory)
        {
            bool result = false;
            try
            {
                result = (1 == _inventoryAccessor.UpdateInventoryItem(inventory));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Item could not be edited/updated.", ex);
            }
            return result;
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/25
        /// 
        /// Calls the RetrieveAllInventoryItems() method
        /// from the DataAccessLayer to display all
        /// inventory items.
        /// </summary>
        /// <returns></returns>
        public List<Inventory> RetrieveAllInventoryItems()
        {
            List<Inventory> items = null;

            try
            {
                items = _inventoryAccessor.SelectAllInventoryItems();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data Unavailable.", ex);
            }
            return items;
        }
    }
}
