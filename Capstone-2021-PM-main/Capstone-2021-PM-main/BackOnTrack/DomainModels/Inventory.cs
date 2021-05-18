using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    /// <summary>
    /// Thomas Stout
    /// Created: 2021/02/12
    /// 
    /// Storage model for Inventory Items
    /// </summary>
    public class Inventory // storage model
    {
        public int InventoryID { get; set; } // Unique integer identifier for an inventory item
        public int InventoryQuantity { get; set; } // Number of items under a particular inventoryID
        public string ItemName { get; set; } // Name of an inventory Item
        public string ItemDescription { get; set; } // Description of an item
    }
}
