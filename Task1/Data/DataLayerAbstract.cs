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

        public static DataLayerAbstract CreateMyDataLayer(DataContext dataContext)
        {
            return new MyDataLayer(dataContext);
        }

        #region Customer
        public abstract void AddCustomer(Customer customer);
        public abstract bool DoesCustomerExist(int id);
        public abstract Customer GetCustomer(int id);
        public abstract bool RemoveCustomer(int id);
        public abstract List<Customer> GetAllCustomers();
        #endregion Customer

        #region Supplier
        public abstract void AddSupplier(Supplier suplier);
        public abstract bool DoesSupplierExists(int id);
        public abstract Supplier GetSupplier(int id);
        public abstract List<Supplier> GetAllSuppliers();
        public abstract bool RemoveSupplier(int id);

        #endregion

        #region State
        public abstract void AddWarehouseEntry(WarehouseEntry entry);
        public abstract bool DoesWarehouseEntryExist(int id);
        public abstract WarehouseEntry GetWarehouseEntry(int id);
        public abstract List<WarehouseEntry> GetAllWarehouseEntries();
        public abstract bool RemoveWarehouseEntry(int id);
        #endregion

        #region Catalog
        public abstract void AddCatalogItem(Product product);
        public abstract bool DoesCatalogItemExist(int id);
        public abstract bool RemoveCatalogItem(int id);
        public abstract Product GetCatalogItem(int id);
        public abstract List<Product> GetAllCatalogItems();

        #endregion

        #region Events
        public abstract void AddDeliveryEvent(Supplier supplier, Product deliveredProduct, int quantity);
        public abstract void AddSoldEvent(Customer customer, Product soldProduct, int quantity);
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

            public override void AddCatalogItem(Product product)
            {
                bool isIdFree = false;
                while(!isIdFree)
                {
                    isIdFree = true;
                    foreach (Product catalogItem in GetAllCatalogItems())
                    {
                        if (catalogItem.Id == product.Id)
                        {
                            isIdFree = false;
                            product.Id++;
                        }
                    }
                }
                
                dataContext.catalog.Add(product);
            }

            public override void AddCustomer(Customer customer)
            {
                bool isIdFree = false;
                while(!isIdFree)
                {
                    isIdFree = true;
                    foreach (Customer registeredCustomer in GetAllCustomers())
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
                    dataContext.customers.Add(customer);
                }
            }

            public override void AddDeliveryEvent(Supplier supplier, Product deliveredProduct, int quantity)
            {
                dataContext.events.Add(new EventDelivery(supplier, deliveredProduct, quantity));
            }

            public override void AddSoldEvent(Customer customer, Product soldProduct, int quantity)
            {
                dataContext.events.Add(new EventSold(customer, soldProduct, quantity));
            }

            public override void AddSupplier(Supplier supplier)
            {
                dataContext.suppliers.Add(supplier);
            }

            public override void AddWarehouseEntry(WarehouseEntry newEntry)
            {
                bool doesEntryAlreadyExist = false;
                foreach(WarehouseEntry entry in GetAllWarehouseEntries())
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
                    dataContext.warehouseState.Add(newEntry);
                }
            }

            public override bool DoesCatalogItemExist(int id)
            {
                foreach(Product product in dataContext.catalog)
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
                foreach(Customer customer in dataContext.customers)
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
                foreach (Supplier supplier in dataContext.suppliers)
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
                foreach(WarehouseEntry entry in  dataContext.warehouseState)
                {
                    if( entry.Id == id)
                    {
                        return true;
                    }
                }
                return false;
            }

            public override List<Product> GetAllCatalogItems()
            {
                return dataContext.catalog;
            }

            public override List<Customer> GetAllCustomers()
            {
                return dataContext.customers;
            }

            public override List<Supplier> GetAllSuppliers()
            {
                return dataContext.suppliers;
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
        }
    }
}
