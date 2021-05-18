using DataAccessInterfaces;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    /// <summary>
    /// Thomas Stout
    /// Created: 2021/02/17
    /// 
    /// Fake item test data.
    public class InventoryFake : IInventoryAccessor
    {
        List<Inventory> data = new List<Inventory>();

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/17
        /// 
        /// Empty constructor with fake data
        /// </summary>
        public InventoryFake()
        {
            data.Add(new Inventory()
            {
                InventoryID = 0,
                InventoryQuantity = 1,
                ItemName = "Shoes",
                ItemDescription = "Shaq Athletic Basketball Sneaker, Boys, Size: 5"
            });
            data.Add(new Inventory()
            {
                InventoryID = 1,
                InventoryQuantity = 20,
                ItemName = "Canned Food",
                ItemDescription = "Great Value Cream Style Sweet Corn, 14.75 oz"
            });
            data.Add(new Inventory()
            {
                InventoryID = 2,
                InventoryQuantity = 100,
                ItemName = "Notebook",
                ItemDescription = "Five Star Notebook, 1 Subject College Ruled"
            });
            data.Add(new Inventory()
            {
                InventoryID = 3,
                InventoryQuantity = 100,
                ItemName = "Pencil",
                ItemDescription = "Ticonderoga 24 Count Yellow Woodcase Pencil"
            });
            data.Add(new Inventory()
            {
                InventoryID = 4,
                InventoryQuantity = 50,
                ItemName = "Socks",
                ItemDescription = "Hanes Womens Ankle Cushion Socks, 12 Pack"
            });
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/23
        /// 
        /// Method that returns the data's row/item count -1
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        public int DeleteInventoryItem(int inventoryID)
        {
            int itemCount = data.Count;
            itemCount -= 1;
            return itemCount;
        }

        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/17
        /// 
        /// Method that returns the data's row/item count +1
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        public int InsertInventoryItem(Inventory inventory)
        {
            return data.Count+1;
        }
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/17
        /// 
        /// Method that returns the data count(all
        /// inventory items).
        /// </summary>
        /// <returns></returns>
        public List<Inventory> SelectAllInventoryItems()
        {
            return data;
        }
        /// <summary>
        /// Thomas Stout
        /// Created: 2021/02/25
        /// 
        /// Method that updates an inventory item.
        /// Returns data.Count because no items
        /// are added or removed.
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        public int UpdateInventoryItem(Inventory inventory)
        {
            return data.Count;
        }
    }
}
