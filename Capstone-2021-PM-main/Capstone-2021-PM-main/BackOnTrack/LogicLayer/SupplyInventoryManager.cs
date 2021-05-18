using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataAccessLayer;
using DomainModels;
using LogicInterfaces;

namespace LogicLayer
{
    public class SupplyInventoryManager : ISupplyInventoryManager
    {
        private ISupplyInventoryAccessor _supplyAccessor = new SupplyInventoryAccessor();

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/05
        /// 
        /// Empty constructor for initializing the SupplyManager
        /// </summary>
        public SupplyInventoryManager()
        {
            _supplyAccessor = new SupplyInventoryAccessor();
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/05
        /// 
        /// Constructor for allowing the initialization of a SupplyAccessor
        /// </summary>
        /// <param name="supplyAccessor"></param>
        public SupplyInventoryManager(ISupplyInventoryAccessor supplyAccessor) // Dependency Injection
        {
            _supplyAccessor = supplyAccessor;
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/05
        /// Add supply item to fake/backontrack_db
        /// </summary>
        /// <param name="supplyItemId"></param>
        /// <param name="supplySerialNum"></param>
        /// <param name="materialName"></param>
        /// <param name="supplyDescription"></param>
        /// <param name="supplyInventoryQuantity"></param>
        /// <returns></returns>
        public bool AddSupplyItem(SupplyItem supplyItem)
        {
            bool result = false;


            try
            {
                // pass new supply item, result = 1 if item is added successfully
                result = (1 == _supplyAccessor.AddSupplyItem(supplyItem));
                if (!result)
                {
                    throw new ApplicationException("Insertion Failed.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Supply item not added.", ex);
            }

            return result;
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/12
        /// Delete supply item from backontrack_db SupplyInventory table
        /// </summary>
        /// <param name="supplyItemID"></param>
        public bool DeleteSupplyItem(int supplyItemID)
        {
            bool result = false;

            try
            {
                // pass selected SupplyItemID, result = 1 if item is deleted successfully
                result = (1 == _supplyAccessor.DeleteSupplyItem(supplyItemID));
                if (!result)
                {
                    throw new ApplicationException("Deletion Failed.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Supply item not deleted.", ex);
            }
            return result;
        }

        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/12
        /// 
        /// updates old SupplyItem record with new record
        /// 
        /// <param name="oldSupplyItem"></param>
        /// <param name="newSupplyItem"></param>
        /// </summary>
        public bool EditSupplyItem(SupplyItem oldSupplyItem, SupplyItem newSupplyItem)
        {
            bool result = true;
            try
            {
                // pass old and new supply items, result = 1 if item is updated successfully
                result = (1 == _supplyAccessor.EditSupplyItem(oldSupplyItem, newSupplyItem));
                if (!result)
                {
                    throw new ApplicationException("Insertion Failed.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Supply item not updated.", ex);
            }
            return result;
        }


        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/03/05
        /// Displays all SupplyInventory items from fakes/backontrack_db
        /// </summary>
        /// <returns></returns>
        public List<SupplyItem> ShowSupplyInventory()
        {
            return _supplyAccessor.GetSupplyInventory();
        }

    }
}
