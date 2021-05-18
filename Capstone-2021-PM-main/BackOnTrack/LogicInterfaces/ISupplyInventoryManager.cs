using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModels;

namespace LogicInterfaces
{
    public interface ISupplyInventoryManager
    {
        bool AddSupplyItem(SupplyItem supplyItem);
        bool EditSupplyItem(SupplyItem oldSupplyItem, SupplyItem newSupplyItem);
        List<SupplyItem> ShowSupplyInventory();
        bool DeleteSupplyItem(int supplyItemID);
    }
}
