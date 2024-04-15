using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class DataLayerAbstract
    {
        #region Customer
        public abstract void AddCustomer(Customer customer);
        public abstract Customer GetCustomer(int id);
        public abstract bool RemoveCustomer(int id);
        public abstract List<Customer> GetAllCustomers();
        #endregion Customer

        #region State
        public abstract void AddWarehouseEntry(WarehouseEntry entry);
        public abstract WarehouseEntry GetWarehouseEntry(int id);
        public abstract List<WarehouseEntry> GetAllWarehouseEntries();
        public abstract bool RemoveWarehouseEntry(int id);
        #endregion

        #region Catalog
        public abstract void AddCatalogItem(Product product);
        public abstract bool RemoveCatalogItem(int id);
        public abstract Product GetCatalogItem(int id);
        public abstract List<Product> GetAllCatalogItems();

        #endregion

        #region Events
        public abstract void AddDeliveryEvent(Product deliveredProduct, int quantity);
        public abstract void AddSoldEvent(Customer customer, Product soldProduct, int quantity);
        #endregion
        private class MyDataLayer : DataLayerAbstract
        {
            public DataContext dataContext;

            public MyDataLayer()
            {
                dataContext = new DataContext();
                throw new NotImplementedException();
            }

            public override void AddCatalogItem(Product product)
            {
                foreach(Product catalogItem in GetAllCatalogItems())
                {
                    if(catalogItem.Id == product.Id)
                    {
                        throw new("Product with given ID already exists.");
                    }
                }
                dataContext.catalog.Add(product);
            }

            public override void AddCustomer(Customer customer)
            {
                foreach (Customer registeredCustomer in GetAllCustomers())
                {
                    if (customer.Id == registeredCustomer.Id)
                    {
                        throw new("Customer with given ID already exists.");
                    }
                }
                dataContext.customers.Add(customer);
            }

            public override void AddDeliveryEvent(Product deliveredProduct, int quantity)
            {
                dataContext.events.Add(new EventDelivery(deliveredProduct, quantity));
            }

            public override void AddSoldEvent(Customer customer, Product soldProduct, int quantity)
            {
                dataContext.events.Add(new EventSold(customer, soldProduct, quantity));
            }

            public override void AddWarehouseEntry(WarehouseEntry newEntry)
            {
                foreach(WarehouseEntry entry in GetAllWarehouseEntries())
                {
                    if(entry.Product == newEntry.Product)
                    {
                        throw new("Given product already has an entry in warehouse.");
                    }
                }
                dataContext.warehouseState.Add(newEntry);
            }

            public override List<Product> GetAllCatalogItems()
            {
                return dataContext.catalog;
            }

            public override List<Customer> GetAllCustomers()
            {
                return dataContext.customers;
            }

            public override List<WarehouseEntry> GetAllWarehouseEntries()
            {
                return dataContext.warehouseState;
            }

            public override Product GetCatalogItem(int id)
            {
                Product? foundProduct = dataContext.catalog.Find(p => p.Id == id);
                if(foundProduct != null) 
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
                Customer? foundCustomer = dataContext.customers.Find(p => p.Id == id);
                if (foundCustomer != null)
                {
                    return foundCustomer;
                }
                else
                {
                    throw new Exception("Customer with given ID does not exits.");
                }
            }

            public override WarehouseEntry GetWarehouseEntry(int id)
            {
                WarehouseEntry? foundEntry = dataContext.warehouseState.Find(p => p.Product.Id == id);
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
                foreach(Product catalogItem in  dataContext.catalog) 
                {
                    if(catalogItem.Id == id)
                    {
                        return dataContext.catalog.Remove(catalogItem);
                    }
                }
                return false;
            }

            public override bool RemoveCustomer(int id)
            {
                foreach (Customer customer in dataContext.customers)
                {
                    if (customer.Id == id)
                    {
                        return dataContext.customers.Remove(customer);
                    }
                }
                return false;
            }

            public override bool RemoveWarehouseEntry(int id)
            {
                foreach (WarehouseEntry entry in dataContext.warehouseState)
                {
                    if (entry.Product.Id == id)
                    {
                        return dataContext.warehouseState.Remove(entry);
                    }
                }
                return false;
            }
        }
    }
}
