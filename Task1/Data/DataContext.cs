using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DataContext
    {
        internal List<Customer> customers;
        internal List<Event> events;
        internal List<Product> catalog;
        internal List<WarehouseEntry> warehouseState;

        internal DataContext()
        {
            customers = new List<Customer>();
            events = new List<Event>();
            catalog = new List<Product>();
            warehouseState = new List<WarehouseEntry>();
        }
    }
}
