using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    internal interface IDataGenerator
    {
        public List<Product> GenerateProducts();
        public List<Customer> GenerateCustomers();

        public List<Supplier> GenerateSuppliers();
        public List<Event> GenerateEvents();

        public List<WarehouseEntry> GenerateWarehouseEntries();
    }
}
