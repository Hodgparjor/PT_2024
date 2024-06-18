using Data;


namespace DataTests
{
    internal class StandardDataGenerator : IDataGenerator
    {
        List<Customer> generatedCustomers;
        List<Product> generatedProducts;
        List<Event> generatedEvents;
        List<Supplier> generatedSuppliers;
        List<WarehouseEntry> generatedWarehouseEntries;

        public StandardDataGenerator()
        {
            generatedCustomers = GenerateCustomers();
            generatedProducts = GenerateProducts();
            generatedSuppliers = GenerateSuppliers();
            generatedWarehouseEntries = GenerateWarehouseEntries();
            generatedEvents = GenerateEvents();
        }

        public List<Customer> GenerateCustomers()
        {
            if (generatedCustomers == null)
            {
                generatedCustomers = new List<Customer>
                {
                    new Customer(1, "John Doe"),
                    new Customer(2, "Jane Doe"),
                    new Customer(3, "Jan Kowalski"),
                    new Customer(4, "Joanna Kowalska")
                };
            }
            else if (generatedCustomers.Count == 0)
            {
                generatedCustomers = new List<Customer>
                {
                    new Customer(1, "John Doe"),
                    new Customer(2, "Jane Doe"),
                    new Customer(3, "Jan Kowalski"),
                    new Customer(4, "Joanna Kowalska")
                };
            }

            return generatedCustomers;
        }

        public List<Product> GenerateProducts()
        {
            if (generatedProducts == null)
            {
                generatedProducts = new List<Product>
                {
                    new Product(1, "ERC18", (decimal)99.9),
                    new Product(2, "ER505", (decimal)98.5),
                    new Product(3, "EH5", (decimal)34.55)
                };
            }
            else if (generatedProducts.Count == 0)
            {
                generatedProducts = new List<Product>
                {
                    new Product(1, "ERC18", (decimal)99.9),
                    new Product(2, "ER505", (decimal)98.5),
                    new Product(3, "EH5", (decimal)34.55)
                };
            }
            return generatedProducts;
        }

        public List<Supplier> GenerateSuppliers()
        {
            if (generatedSuppliers == null)
            {
                generatedSuppliers = new List<Supplier>
            {
                new Supplier(1, "Kankom"),
                new Supplier(2, "Poltech"),
                new Supplier(3, "Stilpo"),
                new Supplier(4, "Tekeko")
            };
            }
            else if (generatedSuppliers.Count == 0)
            {
                generatedSuppliers = new List<Supplier>
            {
                new Supplier(1, "Kankom"),
                new Supplier(2, "Poltech"),
                new Supplier(3, "Stilpo"),
                new Supplier(4, "Tekeko")
            };
            }


            return generatedSuppliers;
        }

        public List<WarehouseEntry> GenerateWarehouseEntries()
        {
            if (generatedWarehouseEntries == null)
            {
                generatedWarehouseEntries = new List<WarehouseEntry>
            {
                new WarehouseEntry(generatedProducts[0], 56),
                new WarehouseEntry(generatedProducts[1], 5),
                new WarehouseEntry(generatedProducts[2], 124)
            };
            }
            else if (generatedWarehouseEntries.Count == 0)
            {
                generatedWarehouseEntries = new List<WarehouseEntry>
            {
                new WarehouseEntry(generatedProducts[0], 56),
                new WarehouseEntry(generatedProducts[1], 5),
                new WarehouseEntry(generatedProducts[2], 124)
            };
            }

            return generatedWarehouseEntries;
        }

        public List<Event> GenerateEvents()
        {
            if (generatedEvents == null)
            {
                generatedEvents = new List<Event>
            {
                new EventSold(generatedCustomers[0], generatedProducts[0], 1),
                new EventSold(generatedCustomers[1], generatedProducts[1], 2),
                new EventDelivery(generatedSuppliers[0], generatedProducts[0], 12)
            };
            }
            else if (generatedEvents.Count == 0)
            {
                generatedEvents = new List<Event>
            {
                new EventSold(generatedCustomers[0], generatedProducts[0], 1),
                new EventSold(generatedCustomers[1], generatedProducts[1], 2),
                new EventDelivery(generatedSuppliers[0], generatedProducts[0], 12)
            };
            }

            return generatedEvents;
        }

    }
}
