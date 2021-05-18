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
    /// Richard Schroeder
    /// Created: 2021/03/05
    /// 
    /// Fake SupplyItem test data.
    /// </summary>
    public class SupplyItemFake : ISupplyInventoryAccessor
    {
        private List<SupplyItem> _supplyItems = new List<SupplyItem>();

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/05
        /// 
        /// Empty constructor that initializes fake SupplyItem data.
        /// </summary>
        public SupplyItemFake()
        {
            _supplyItems.Add(new SupplyItem
            {
                SupplyItemID = 100000,
                SupplySerialNumber = 123456,
                MaterialName = "Packing Tape",
                SupplyDescription = "Tape for packing boxes",
                SupplyInventoryQuantity = 200
            });

            _supplyItems.Add(new SupplyItem
            {
                SupplyItemID = 100001,
                SupplySerialNumber = 987654,
                MaterialName = "12x12in Box",
                SupplyDescription = "12x12in Box for packing",
                SupplyInventoryQuantity = 1000
            });

            _supplyItems.Add(new SupplyItem
            {
                SupplyItemID = 100002,
                SupplySerialNumber = 134675,
                MaterialName = "15in Bubble Wrap",
                SupplyDescription = "15in wide bubble wrap roll",
                SupplyInventoryQuantity = 150
            });
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/05
        /// 
        /// A method for adding a supply item to the database
        /// </summary>
        public int AddSupplyItem(SupplyItem supplyItem)
        {
            int result = 0;

            try
            {
                _supplyItems.Add(supplyItem);
                if (_supplyItems.Contains(supplyItem))
                {
                    result = 1;
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/05
        /// 
        /// A method for checking if duplicate serial number exists
        /// </summary>
        public bool AuthenticateSerialNumber(int supplySerialNum)
        {
            bool result = false;
            try
            {
                foreach (var si in _supplyItems)
                {
                    if (si.SupplySerialNumber == supplySerialNum)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/12
        /// 
        /// A method for deleting supply items from fakes
        /// </summary>
        public int DeleteSupplyItem(int supplyItemID)
        {
            int result = 0;

            try
            {
                foreach (var si in _supplyItems)
                {
                    if (si.SupplyItemID == supplyItemID)
                    {
                        _supplyItems.Remove(si);
                        result = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/12
        /// 
        /// Method for updating existing fake supply items with new information
        /// </summary>
        /// <returns></returns>
        public int EditSupplyItem(SupplyItem oldSupplyItem, SupplyItem newSupplyItem)
        {
            int result = 0;

            try
            {
                if (newSupplyItem.SupplySerialNumber < 100000 || newSupplyItem.SupplySerialNumber > 999999)
                {
                    throw new ApplicationException();
                }
                else
                {
                    foreach (var si in _supplyItems)
                    {
                        if (si.SupplyItemID == newSupplyItem.SupplyItemID)
                        {
                            si.SupplySerialNumber = newSupplyItem.SupplySerialNumber;
                            si.SupplyInventoryQuantity = newSupplyItem.SupplyInventoryQuantity;
                            si.SupplyDescription = newSupplyItem.SupplyDescription;
                            si.MaterialName = newSupplyItem.MaterialName;
                            result = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/05
        /// 
        /// A method for returning a full list of supply items
        /// </summary>
        public List<SupplyItem> GetSupplyInventory()
        {
            try
            {
                return _supplyItems;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
