using Data;

namespace DataTests
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
