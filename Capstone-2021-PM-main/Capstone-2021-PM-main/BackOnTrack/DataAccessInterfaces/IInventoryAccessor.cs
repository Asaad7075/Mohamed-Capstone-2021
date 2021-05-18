using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModels;

namespace DataAccessInterfaces
{
    public interface IInventoryAccessor
    {
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/17
        /// 
        /// Interface method for inserting an inventory item.
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        int InsertInventoryItem(Inventory inventory); // Returns rows affected
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/27
        /// 
        /// Interface method for deleting an inventory item.
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        int DeleteInventoryItem(int inventoryID); // Returns rows affected
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/25
        /// 
        /// Interface method for selecting all inventory items.
        /// </summary>
        /// <returns></returns>
        List<Inventory> SelectAllInventoryItems();
        /// <summary>
        /// Thomas Stout
        /// Created 2021/02/25
        /// 
        /// Interface method for updating/editing
        /// an inventory item. Returns the number 
        /// of rows affected.
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        int UpdateInventoryItem(Inventory inventory);
    }
}
