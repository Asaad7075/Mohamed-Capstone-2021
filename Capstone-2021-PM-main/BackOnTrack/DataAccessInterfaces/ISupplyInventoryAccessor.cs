using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface ISupplyInventoryAccessor
    {
        int AddSupplyItem(SupplyItem supplyItem);
        int EditSupplyItem(SupplyItem oldSupplyItem, SupplyItem newSupplyItem);
        List<SupplyItem> GetSupplyInventory();
        int DeleteSupplyItem(int supplyItemID);

    }
}
