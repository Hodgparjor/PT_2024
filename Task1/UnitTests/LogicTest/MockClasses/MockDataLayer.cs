using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace UnitTests.LogicTest.MockClasses
{
    internal class MockDataLayer : DataLayerAbstract
    {
        List<Product> products;
        List<WarehouseEntry> warehouseEntries;
        List<Customer> customers;
        List<Supplier> suppliers;
        public bool deliveryEventCreated = false;
        public bool saleEventCreated = false;
        public MockDataLayer()
        {
            products = new List<Product>();
            warehouseEntries = new List<WarehouseEntry>();
            customers = new List<Customer>();
            suppliers = new List<Supplier>();
        }
        public override void AddCatalogItem(Product product)
        {
            products.Add(product);
        }

        public override void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }

        public override void AddDeliveryEvent(Supplier supplier, Product deliveredProduct, int quantity)
        {
            deliveryEventCreated = true;
        }

        public override void AddSoldEvent(Customer customer, Product soldProduct, int quantity)
        {
            saleEventCreated = true;
        }

        public override void AddSupplier(Supplier suplier)
        {
            suppliers.Add(suplier);
        }

        public override void AddWarehouseEntry(WarehouseEntry entry)
        {
            warehouseEntries.Add(entry);
        }

        public override bool DoesCatalogItemExist(int id)
        {
            foreach (Product product in products)
            {
                if (product.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public override bool DoesCustomerExist(int id)
        {
            foreach (Customer customer in customers)
            {
                if (customer.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public override bool DoesSupplierExists(int id)
        {
            foreach (Supplier supplier in suppliers)
            {
                if (supplier.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public override bool DoesWarehouseEntryExist(int id)
        {
            foreach (WarehouseEntry entry in warehouseEntries)
            {
                if (entry.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public override List<Product> GetAllCatalogItems()
        {
            return products;
        }

        public override List<Customer> GetAllCustomers()
        {
            return customers;
        }

        public override List<Supplier> GetAllSuppliers()
        {
            return suppliers;
        }

        public override List<WarehouseEntry> GetAllWarehouseEntries()
        {
            return warehouseEntries;
        }

        public override Product GetCatalogItem(int id)
        {
            Product? foundProduct = products.Find(p => p.Id == id);
            if (foundProduct != null)
            {
                return foundProduct;
            }
            else
            {
                throw new Exception("Product with given ID does not exits.");
            }
        }

        public override Customer GetCustomer(int id)
        {
            Customer? foundCustomer = customers.Find(p => p.Id == id);
            if (foundCustomer != null)
            {
                return foundCustomer;
            }
            else
            {
                throw new Exception("Customer with given ID does not exits.");
            }
        }

        public override Supplier GetSupplier(int id)
        {
            Supplier? foundSupplier = suppliers.Find(p => p.Id == id);
            if (foundSupplier != null)
            {
                return foundSupplier;
            }
            else
            {
                throw new Exception("Customer with given ID does not exits.");
            }
        }

        public override WarehouseEntry GetWarehouseEntry(int id)
        {
            WarehouseEntry? foundEntry = warehouseEntries.Find(p => p.Id == id);
            if (foundEntry != null)
            {
                return foundEntry;
            }
            else
            {
                throw new Exception("Product with given ID does not have warehouse entry.");
            }
        }

        public override bool RemoveCatalogItem(int id)
        {
            return products.Remove(GetCatalogItem(id));
        }

        public override bool RemoveCustomer(int id)
        {
            return customers.Remove(GetCustomer(id));
        }

        public override bool RemoveSupplier(int id)
        {
            return suppliers.Remove(GetSupplier(id));
        }

        public override bool RemoveWarehouseEntry(int id)
        {
            return warehouseEntries.Remove(GetWarehouseEntry(id));
        }
    }
}
