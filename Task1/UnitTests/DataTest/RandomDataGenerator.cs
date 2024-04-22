using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.DataTest
{
    internal class RandomDataGenerator : IDataGenerator
    {
        List<Customer> generatedCustomers;
        List<Product> generatedProducts;
        List<Event> generatedEvents;
        List<Supplier> generatedSuppliers;
        List<WarehouseEntry> generatedWarehouseEntries;

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
            throw new NotImplementedException();
        }

        public List<Event> GenerateEvents()
        {
            throw new NotImplementedException();
        }

        public List<Product> GenerateProducts()
        {
            throw new NotImplementedException();
        }

        public List<Supplier> GenerateSuppliers()
        {
            throw new NotImplementedException();
        }

        public List<WarehouseEntry> GenerateWarehouseEntries()
        {
            throw new NotImplementedException();
        }
    }
}
