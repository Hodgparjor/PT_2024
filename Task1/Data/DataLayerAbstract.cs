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
        #endregion Customer

        #region Supplier
        public abstract void AddSupplier(ISupplier suplier);
        public abstract bool DoesSupplierExists(int id);
        public abstract ISupplier GetSupplier(int id);
        public abstract List<ISupplier> GetAllSuppliers();
        public abstract bool RemoveSupplier(int id);

        #endregion

        #region State
        public abstract void AddWarehouseEntry(IWarehouseEntry entry);
        public abstract bool DoesWarehouseEntryExist(int id);
        public abstract IWarehouseEntry GetWarehouseEntry(int id);
        public abstract List<IWarehouseEntry> GetAllWarehouseEntries();
        public abstract bool RemoveWarehouseEntry(int id);
        #endregion

        #region Catalog
        public abstract void AddCatalogItem(IProduct product);
        public abstract bool DoesCatalogItemExist(int id);
        public abstract bool RemoveCatalogItem(int id);
        public abstract IProduct GetCatalogItem(int id);
        public abstract List<IProduct> GetAllCatalogItems();

        #endregion

        #region Events
        public abstract void AddDeliveryEvent(ISupplier supplier, IProduct deliveredProduct, int quantity);
        public abstract void AddSoldEvent(ICustomer customer, IProduct soldProduct, int quantity);
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
                    dataContext.customers.Add((Customer)customer);
                }
            }

            public override void AddDeliveryEvent(ISupplier supplier, IProduct deliveredProduct, int quantity)
            {
                dataContext.events.Add(new EventDelivery((Supplier)supplier, (Product)deliveredProduct, quantity));
            }

            public override void AddSoldEvent(ICustomer customer, IProduct soldProduct, int quantity)
            {
                dataContext.events.Add(new EventSold((Customer)customer, (Product)soldProduct, quantity));
            }

            public override void AddSupplier(ISupplier supplier)
            {
                dataContext.suppliers.Add((Supplier)supplier);
            }

            public override void AddWarehouseEntry(IWarehouseEntry newEntry)
            {
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

            public override List<ISupplier> GetAllSuppliers()
            {
                return dataContext.suppliers.ToList<ISupplier>();
            }

            public override List<IWarehouseEntry> GetAllWarehouseEntries()
            {
                return dataContext.warehouseState.ToList<IWarehouseEntry>();
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
