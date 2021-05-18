using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class SupplyItem
    {
        public int SupplyItemID { get; set; }
        public int SupplySerialNumber { get; set; }
        public string MaterialName { get; set; }
        public string SupplyDescription { get; set; }
        public int SupplyInventoryQuantity { get; set; }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/05
        /// 
        /// Constructor for creating new SupplyItem objects
        /// </summary>
        public SupplyItem(int supplyItemId, int supplySerialNum, string materialName, string supplyDescription, int supplyInventoryQuantity)
        {
            SupplyItemID = supplyItemId;
            SupplySerialNumber = supplySerialNum;
            MaterialName = materialName;
            SupplyDescription = supplyDescription;
            SupplyInventoryQuantity = supplyInventoryQuantity;
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/05
        /// 
        /// Default constructor for creating new SupplyItem objects
        /// </summary>
        public SupplyItem()
        {

        }
    }
}
