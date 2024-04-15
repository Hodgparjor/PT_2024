using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class EventSold : Event
    {
        private Customer customer;
        private List<WarehouseEntry> warehouseEntries;

        internal Customer Customer { get => customer; set => customer = value; }
        internal List<WarehouseEntry> WarehouseEntries { get => warehouseEntries; set => warehouseEntries = value; }

    }
}
