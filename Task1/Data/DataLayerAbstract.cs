using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public abstract class DataLayerAbstract
    {
        public static DataLayerAbstract CreateMyDataLayer()
        {
            return new MyDataLayer();
        }

        public static DataLayerAbstract CreateDatabase()
        {
            throw new NotImplementedException();
        }

        public static DataLayerAbstract CreateMyDataLayer(DataContext dataContext)
        {
            return new MyDataLayer(dataContext);
        }

        #region Customer
        public abstract void AddCustomer(ICustomer customer);
        public abstract bool DoesCustomerExist(int id);
        public abstract ICustomer GetCustomer(int id);
        public abstract bool RemoveCustomer(int id);
        public abstract List<ICustomer> GetAllCustomers();
        public abstract Task AddCustomerAsync(int id, string name);
        public abstract Task<ICustomer> GetCustomerAsync(int id);
        public abstract Task<Dictionary<int, ICustomer>> GetAllCustomersAsync();
        public abstract Task UpdateCustomerAsync(int id, string name);
        public abstract Task DeleteCustomerAsync(int id);
        public abstract Task<int> GetCustomerCountAsync();

        #endregion Customer

        #region Supplier
        public abstract void AddSupplier(ISupplier suplier);
        public abstract bool DoesSupplierExists(int id);
        public abstract ISupplier GetSupplier(int id);
        public abstract List<ISupplier> GetAllSuppliers();
        public abstract bool RemoveSupplier(int id);

        #endregion

        #region State
        public abstract void AddWarehouseEntry(IProduct product, int quantity);
        public abstract bool DoesWarehouseEntryExist(int id);
        public abstract IWarehouseEntry GetWarehouseEntry(int id);
        public abstract List<IWarehouseEntry> GetAllWarehouseEntries();
        public abstract bool RemoveWarehouseEntry(int id);
        public abstract Task AddWarehouseEntryAsync(int id, int productId, int quantity);
        public abstract Task<IWarehouseEntry> GetWarehouseEntryAsync(int id);
        public abstract Task UpdateWarehouseEntryAsync(int id, int productId, int quantity);
        public abstract Task DeleteWarehouseEntryAsync(int id);
        public abstract Task<Dictionary<int, IWarehouseEntry>> GetAllWarehouseEntriesAsync();
        public abstract Task<int> GetWarehouseEntriesCountAsync();

        #endregion

        #region Catalog
        public abstract void AddCatalogItem(IProduct product);
        public abstract bool DoesCatalogItemExist(int id);
        public abstract bool RemoveCatalogItem(int id);
        public abstract IProduct GetCatalogItem(int id);
        public abstract List<IProduct> GetAllCatalogItems();
        public abstract Task AddProductAsync(int id, string name, decimal price);
        public abstract Task<IProduct> GetProductAsync(int id);
        public abstract Task UpdateProductAsync(int id, string name, decimal price);
        public abstract Task DeleteProductAsync(int id);
        public abstract Task<Dictionary<int, IProduct>> GetAllProductsAsync();
        public abstract Task<int> GetProductsCountAsync();

        #endregion

        #region Events
        public abstract void AddDeliveryEvent(ISupplier supplier, IProduct deliveredProduct, int quantity);
        public abstract void AddSoldEvent(int customer, int soldProduct, int quantity);
        public abstract Task AddEventAsync(int id, int stateId, int userId, int quantity);
        public abstract Task<IEventSold> GetEventAsync(int id);
        public abstract Task UpdateEventAsync(int id, int stateId, int userId, DateTime occurenceDate, int quantity);
        public abstract Task DeleteEventAsync(int id);
        public abstract Task<Dictionary<int, IEventSold>> GetAllEventsAsync();
        public abstract Task<int> GetEventsCountAsync();
        #endregion
        private class MyDataLayer : DataLayerAbstract
        {
            public DataContext dataContext;

            public MyDataLayer()
            {
                dataContext = new DataContext();
            }

            public MyDataLayer(DataContext dataContext)
            {
                this.dataContext = dataContext;
            }

            public override void AddCatalogItem(IProduct product)
            {
                bool isIdFree = false;
                while(!isIdFree)
                {
                    isIdFree = true;
                    foreach (IProduct catalogItem in GetAllCatalogItems())
                    {
                        if (catalogItem.Id == product.Id)
                        {
                            isIdFree = false;
                            product.Id++;
                        }
                    }
                }
                
                dataContext.catalog.Add((Product)product);
            }

            public override void AddCustomer(ICustomer customer)
            {
                bool isIdFree = false;
                while(!isIdFree)
                {
                    isIdFree = true;
                    foreach (ICustomer registeredCustomer in GetAllCustomers())
                    {
                        if (customer.Id == registeredCustomer.Id)
                        {
                            isIdFree = false;
                            customer.Id++;
                        }
                    }
                }
                
                if(isIdFree)
                {
                    dataContext.customers.Add((Customer)customer);
                }
            }

            public override Task AddCustomerAsync(int id, string name)
            {
                throw new NotImplementedException();
            }

            public override void AddDeliveryEvent(ISupplier supplier, IProduct deliveredProduct, int quantity)
            {
                dataContext.events.Add(new EventDelivery((Supplier)supplier, (Product)deliveredProduct, quantity));
            }

            public override Task AddEventAsync(int id, int stateId, int userId, int quantity)
            {
                throw new NotImplementedException();
            }

            public override Task AddProductAsync(int id, string name, decimal price)
            {
                throw new NotImplementedException();
            }

            public override void AddSoldEvent(int customer, int soldProduct, int quantity)
            {
                dataContext.events.Add(new EventSold((Customer)GetCustomer(customer), (Product)GetCatalogItem(soldProduct), quantity));
            }

            public override void AddSupplier(ISupplier supplier)
            {
                dataContext.suppliers.Add((Supplier)supplier);
            }

            public override void AddWarehouseEntry(IProduct product,  int quantity)
            {
                WarehouseEntry newEntry = new((Product)product, quantity);
                bool doesEntryAlreadyExist = false;
                foreach(IWarehouseEntry entry in GetAllWarehouseEntries())
                {
                    if(entry.Product == newEntry.Product)
                    {
                        entry.Quantity += newEntry.Quantity;
                        return;
                        //throw new("Given product already has an entry in warehouse.");
                    }
                }
                if(!doesEntryAlreadyExist)
                {
                    dataContext.warehouseState.Add((WarehouseEntry)newEntry);
                }
            }

            public override Task AddWarehouseEntryAsync(int id, int productId, int quantity)
            {
                throw new NotImplementedException();
            }

            public override Task DeleteCustomerAsync(int id)
            {
                throw new NotImplementedException();
            }

            public override Task DeleteEventAsync(int id)
            {
                throw new NotImplementedException();
            }

            public override Task DeleteProductAsync(int id)
            {
                throw new NotImplementedException();
            }

            public override Task DeleteWarehouseEntryAsync(int id)
            {
                throw new NotImplementedException();
            }

            public override bool DoesCatalogItemExist(int id)
            {
                foreach(IProduct product in dataContext.catalog)
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
                foreach(ICustomer customer in dataContext.customers)
                {
                    if(customer.Id == id)
                    {
                        return true;
                    }
                }
                return false;
            }

            public override bool DoesSupplierExists(int id)
            {
                foreach (ISupplier supplier in dataContext.suppliers)
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
                foreach(IWarehouseEntry entry in  dataContext.warehouseState)
                {
                    if( entry.Id == id)
                    {
                        return true;
                    }
                }
                return false;
            }

            public override List<IProduct> GetAllCatalogItems()
            {
                return dataContext.catalog.ToList<IProduct>();
            }

            public override List<ICustomer> GetAllCustomers()
            {
                return dataContext.customers.ToList<ICustomer>();
            }

            public override Task<Dictionary<int, ICustomer>> GetAllCustomersAsync()
            {
                throw new NotImplementedException();
            }

            public override Task<Dictionary<int, IEventSold>> GetAllEventsAsync()
            {
                throw new NotImplementedException();
            }

            public override Task<Dictionary<int, IProduct>> GetAllProductsAsync()
            {
                throw new NotImplementedException();
            }

            public override List<ISupplier> GetAllSuppliers()
            {
                return dataContext.suppliers.ToList<ISupplier>();
            }

            public override List<IWarehouseEntry> GetAllWarehouseEntries()
            {
                return dataContext.warehouseState.ToList<IWarehouseEntry>();
            }

            public override Task<Dictionary<int, IWarehouseEntry>> GetAllWarehouseEntriesAsync()
            {
                throw new NotImplementedException();
            }

            public override IProduct GetCatalogItem(int id)
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

            public override Task<ICustomer> GetCustomerAsync(int id)
            {
                throw new NotImplementedException();
            }

            public override Task<int> GetCustomerCountAsync()
            {
                throw new NotImplementedException();
            }

            public override Task<IEventSold> GetEventAsync(int id)
            {
                throw new NotImplementedException();
            }

            public override Task<int> GetEventsCountAsync()
            {
                throw new NotImplementedException();
            }

            public override Task<IProduct> GetProductAsync(int id)
            {
                throw new NotImplementedException();
            }

            public override Task<int> GetProductsCountAsync()
            {
                throw new NotImplementedException();
            }

            public override Supplier GetSupplier(int id)
            {
                Supplier? foundSupplier = dataContext.suppliers.Find(p => p.Id == id);
                if (foundSupplier != null)
                {
                    return foundSupplier;
                }
                else
                {
                    throw new Exception("Customer with given ID does not exits.");
                }
            }

            public override Task<int> GetWarehouseEntriesCountAsync()
            {
                throw new NotImplementedException();
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

            public override Task<IWarehouseEntry> GetWarehouseEntryAsync(int id)
            {
                throw new NotImplementedException();
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

            public override bool RemoveSupplier(int id)
            {
                foreach (Supplier supplier in dataContext.suppliers)
                {
                    if (supplier.Id == id)
                    {
                        return dataContext.suppliers.Remove(supplier);
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

            public override Task UpdateCustomerAsync(int id, string name)
            {
                throw new NotImplementedException();
            }

            public override Task UpdateEventAsync(int id, int stateId, int userId, DateTime occurenceDate, int quantity)
            {
                throw new NotImplementedException();
            }

            public override Task UpdateProductAsync(int id, string name, decimal price)
            {
                throw new NotImplementedException();
            }

            public override Task UpdateWarehouseEntryAsync(int id, int productId, int quantity)
            {
                throw new NotImplementedException();
            }
        }
    }
}
