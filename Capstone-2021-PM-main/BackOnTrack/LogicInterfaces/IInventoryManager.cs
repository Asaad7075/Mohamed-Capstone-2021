using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterfaces
{
    /// <summary>
    /// Thomas Stout
    /// Created: 2021/02/17
    /// 
    /// Interface for Inventory Item Management
    /// </summary>
    public interface IInventoryManager
    {
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/17
        /// 
        /// Adds the item to inventory
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        bool AddInventoryItem(Inventory inventory);

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/25
        /// 
        /// Retrieves all items from inventory
        /// </summary>
        /// <returns></returns>
        List<Inventory> RetrieveAllInventoryItems();
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/25
        /// 
        /// Deletes an item from inventory. Returns
        /// true if item was deleted.
        /// </summary>
        /// <param name="inventoryID"></param>
        /// <returns></returns>
        bool DeleteInventoryItem(int inventoryID);
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/25
        /// 
        /// Edits an inventory item. Returns
        /// true if item was edited/updated.
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        bool EditInventoryItem(Inventory inventory);
    }
}
