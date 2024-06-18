using Data;

namespace DataTests
{
    internal class RandomDataGenerator : IDataGenerator
    {
        List<Customer> generatedCustomers;
        List<Product> generatedProducts;
        List<Event> generatedEvents;
        List<Supplier> generatedSuppliers;
        List<WarehouseEntry> generatedWarehouseEntries;
        Random rnd = new Random();

        List<string> names = new List<string>
        {
            "Jane",
            "Martin",
            "Max",
            "Alex",
            "John",
            "Margaret",
            "Kate",
            "Tim",
            "Ben",
            "Angela"
        };

        List<string> productNames = new List<string>
        {
            "EG1",
            "SDS",
            "ER2",
            "VRS20",
            "SMIT33",
            "K01",
            "WPC156",
            "TSL1234",
            "OMNICONTROL",
            "MASTERCONTROL"
        };

        public RandomDataGenerator()
        {
            generatedCustomers = GenerateCustomers();
            generatedProducts = GenerateProducts();
            generatedSuppliers = GenerateSuppliers();
            generatedWarehouseEntries = GenerateWarehouseEntries();
            generatedEvents = GenerateEvents();
        }

        public List<Customer> GenerateCustomers()
        {
            if (generatedCustomers != null)
            {
                if (generatedCustomers.Count > 0) { return generatedCustomers; }
            }
            generatedCustomers = new List<Customer>();
            for (int i = 0; i < 5; i++)
            {
                generatedCustomers.Add(new Customer(rnd.Next(500), names[rnd.Next(0, names.Count - 1)]));
            }
            return generatedCustomers;
        }

        public List<Event> GenerateEvents()
        {
            if (generatedEvents != null)
            {
                if (generatedEvents.Count > 0)
                {
                    return generatedEvents;
                }
            }
            generatedEvents = new List<Event>();
            for (int i = 0; i < 2; i++)
            {
                generatedEvents.Add(new EventDelivery(generatedSuppliers[i], generatedProducts[i], rnd.Next(40, 100)));
            }
            for (int i = 0; i < 2; i++)
            {
                generatedEvents.Add(new EventSold(generatedCustomers[i], generatedProducts[i], rnd.Next(5, 10)));
            }
            return generatedEvents;
        }

        public List<Product> GenerateProducts()
        {
            if (generatedProducts != null)
            {
                if (generatedProducts.Count > 0) { return generatedProducts; }
            }
            generatedProducts = new List<Product>();
            for (int i = 0; i < 5; i++)
            {
                generatedProducts.Add(new Product(rnd.Next(500), productNames[rnd.Next(0, productNames.Count - 1)], (decimal)9.99));
            }
            return generatedProducts;
        }

        public List<Supplier> GenerateSuppliers()
        {
            if (generatedSuppliers != null)
            {
                if (generatedSuppliers.Count > 0) { return generatedSuppliers; }
            }
            generatedSuppliers = new List<Supplier>();
            for (int i = 0; i < 5; i++)
            {
                generatedSuppliers.Add(new Supplier(rnd.Next(50), names[rnd.Next(0, names.Count - 1)]));
            }
            return generatedSuppliers;
        }

        public List<WarehouseEntry> GenerateWarehouseEntries()
        {
            if (generatedWarehouseEntries != null)
            {
                if (generatedWarehouseEntries.Count > 0) { return generatedWarehouseEntries; }
            }
            generatedWarehouseEntries = new List<WarehouseEntry>();
            for (int i = 0; i < 5; i++)
            {
                generatedWarehouseEntries.Add(new WarehouseEntry(generatedProducts[i], rnd.Next(500)));
            }
            return generatedWarehouseEntries;
        }
    }
}
