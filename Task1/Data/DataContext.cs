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
        internal List<Supplier> suppliers;
        internal List<Event> events;
        internal List<Product> catalog;
        internal List<WarehouseEntry> warehouseState;

        public DataContext()
        {
            customers = new List<Customer>();
            suppliers = new List<Supplier>();
            events = new List<Event>();
            catalog = new List<Product>();
            warehouseState = new List<WarehouseEntry>();
        }

        internal DataContext(List<Customer> customers, List<Supplier> suppliers, List<Event> events, List<Product> catalog, List<WarehouseEntry> warehouseState)
        {
            this.customers = customers;
            this.suppliers = suppliers;
            this.events = events;
            this.catalog = catalog;
            this.warehouseState = warehouseState;
        }
    }
}
