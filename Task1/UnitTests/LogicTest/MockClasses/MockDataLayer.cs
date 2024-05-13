using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Data;

namespace UnitTests.LogicTest.MockClasses
{
    internal class MockDataLayer : DataLayerAbstract
    {
        List<MockProduct> products;
        List<MockWarehouseEntry> warehouseEntries;
        List<MockCustomer> customers;
        List<MockSupplier> suppliers;
        public bool deliveryEventCreated = false;
        public bool saleEventCreated = false;
        public MockDataLayer()
        {
            products = new List<MockProduct>();
            warehouseEntries = new List<MockWarehouseEntry>();
            customers = new List<MockCustomer>();
            suppliers = new List<MockSupplier>();
        }
        public override void AddCatalogItem(IProduct product)
        {
            products.Add((MockProduct)product);
        }

        public override void AddCustomer(ICustomer customer)
        {
            customers.Add((MockCustomer)customer);
        }

        public override void AddDeliveryEvent(ISupplier supplier, IProduct deliveredProduct, int quantity)
        {
            deliveryEventCreated = true;
        }

        public override void AddSoldEvent(ICustomer customer, IProduct soldProduct, int quantity)
        {
            saleEventCreated = true;
        }

        public override void AddSupplier(ISupplier suplier)
        {
            suppliers.Add((MockSupplier)suplier);
        }

        public override void AddWarehouseEntry(IProduct product, int quantity)
        {
            warehouseEntries.Add(new MockWarehouseEntry((MockProduct)product, quantity));
        }

        public override bool DoesCatalogItemExist(int id)
        {
            foreach (IProduct product in products)
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
            foreach (ICustomer customer in customers)
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
            foreach (ISupplier supplier in suppliers)
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
            foreach (IWarehouseEntry entry in warehouseEntries)
            {
                if (entry.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public override List<IProduct> GetAllCatalogItems()
        {
            return products.ToList<IProduct>();
        }

        public override List<ICustomer> GetAllCustomers()
        {
            return customers.ToList<ICustomer>();
        }

        public override List<ISupplier> GetAllSuppliers()
        {
            return suppliers.ToList<ISupplier>();
        }

        public override List<IWarehouseEntry> GetAllWarehouseEntries()
        {
            return warehouseEntries.ToList<IWarehouseEntry>();
        }

        public override IProduct GetCatalogItem(int id)
        {
            IProduct? foundProduct = products.Find(p => p.Id == id);
            if (foundProduct != null)
            {
                return foundProduct;
            }
            else
            {
                throw new Exception("Product with given ID does not exits.");
            }
        }

        public override ICustomer GetCustomer(int id)
        {
            ICustomer? foundCustomer = customers.Find(p => p.Id == id);
            if (foundCustomer != null)
            {
                return foundCustomer;
            }
            else
            {
                throw new Exception("Customer with given ID does not exits.");
            }
        }

        public override ISupplier GetSupplier(int id)
        {
            ISupplier? foundSupplier = suppliers.Find(p => p.Id == id);
            if (foundSupplier != null)
            {
                return foundSupplier;
            }
            else
            {
                throw new Exception("Customer with given ID does not exits.");
            }
        }

        public override IWarehouseEntry GetWarehouseEntry(int id)
        {
            IWarehouseEntry? foundEntry = warehouseEntries.Find(p => p.Id == id);
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
            return products.Remove((MockProduct)GetCatalogItem(id));
        }

        public override bool RemoveCustomer(int id)
        {
            return customers.Remove((MockCustomer)GetCustomer(id));
        }

        public override bool RemoveSupplier(int id)
        {
            return suppliers.Remove((MockSupplier)GetSupplier(id));
        }

        public override bool RemoveWarehouseEntry(int id)
        {
            return warehouseEntries.Remove((MockWarehouseEntry)GetWarehouseEntry(id));
        }
    }
}
