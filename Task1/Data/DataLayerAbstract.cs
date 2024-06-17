using Data.Database;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            return new MyDataLayer();
        }

        public static DataLayerAbstract CreateMyDataLayer(DataContext dataContext)
        {
            return new MyDataLayer(dataContext);
        }

        #region Customer
        public abstract Task AddCustomerAsync(int id, string name);
        public abstract Task<ICustomer> GetCustomerAsync(int id);
        public abstract Task<Dictionary<int, ICustomer>> GetAllCustomersAsync();
        public abstract Task UpdateCustomerAsync(int id, string name);
        public abstract Task DeleteCustomerAsync(int id);
        public abstract Task<int> GetCustomerCountAsync();

        #endregion Customer

        #region State
        public abstract Task AddWarehouseEntryAsync(int id, int productId, int quantity);
        public abstract Task<IWarehouseEntry> GetWarehouseEntryAsync(int id);
        public abstract Task UpdateWarehouseEntryAsync(int id, int productId, int quantity);
        public abstract Task DeleteWarehouseEntryAsync(int id);
        public abstract Task<Dictionary<int, IWarehouseEntry>> GetAllWarehouseEntriesAsync();
        public abstract Task<int> GetWarehouseEntriesCountAsync();

        #endregion

        #region Catalog
        public abstract Task AddProductAsync(int id, string name, decimal price);
        public abstract Task<IProduct> GetProductAsync(int id);
        public abstract Task UpdateProductAsync(int id, string name, decimal price);
        public abstract Task DeleteProductAsync(int id);
        public abstract Task<Dictionary<int, IProduct>> GetAllProductsAsync();
        public abstract Task<int> GetProductsCountAsync();

        #endregion

        #region Events
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



            public async override Task AddCustomerAsync(int id, string name)
            {
                IUser user = new Customer(id, name);

                await this.dataContext.AddCustomerAsync(user);
            }

            public async override Task AddEventAsync(int id, int stateId, int userId, int quantity)
            {
                IUser user = await this.GetCustomerAsync(userId);
                IWarehouseEntry warehouseEntry = await this.GetWarehouseEntryAsync(stateId);
                IEventSold newEvent = new EventSold(id, userId, stateId, DateTime.Now, quantity);

                if (warehouseEntry.Quantity < quantity)
                    throw new Exception("Product has insufficent quantity");


                await this.UpdateWarehouseEntryAsync(stateId, warehouseEntry.ProductId, warehouseEntry.Quantity - quantity);
                await this.dataContext.AddEventAsync(newEvent);
            }

            public async override Task AddProductAsync(int id, string name, decimal price)
            {
                IProduct product = new Product(id, name, price);

                await this.dataContext.AddProductAsync(product);
            }


            public async override Task AddWarehouseEntryAsync(int id, int productId, int quantity)
            {
                if (!await dataContext.DoesProductExists(productId))
                    throw new Exception("This product does not exist!");

                if (quantity < 0)
                    throw new Exception("Product's quantity must be number greater that 0!");

                IWarehouseEntry warehouseEntry = new WarehouseEntry(id, productId, quantity);

                await dataContext.AddWarehouseEntryAsync(warehouseEntry);
            }

            public async override Task DeleteCustomerAsync(int id)
            {
                if (!await dataContext.DoesCustomerExist(id))
                    throw new Exception("This user does not exist");

                await this.dataContext.DeleteCustomerAsync(id);
            }

            public async override Task DeleteEventAsync(int id)
            {
                if (!await dataContext.DoesEventExists(id))
                    throw new Exception("This event does not exist");

                await dataContext.DeleteEventAsync(id);
            }

            public async override Task DeleteProductAsync(int id)
            {
                if (!await dataContext.DoesProductExists(id))
                    throw new Exception("This product does not exist");

                await dataContext.DeleteProductAsync(id);
            }

            public override async Task DeleteWarehouseEntryAsync(int id)
            {
                if (!await dataContext.DoesWarehouseEntryExistsAsync(id))
                    throw new Exception("This warehouse entry does not exits");

                await dataContext.DeleteWarehouseEntryAsync(id);
            }



            public override async Task<Dictionary<int, ICustomer>> GetAllCustomersAsync()
            {
                return await this.dataContext.GetAllCustomersAsync();
            }

            public override async Task<Dictionary<int, IEventSold>> GetAllEventsAsync()
            {
                return await this.dataContext.GetAllEventsAsync();
            }

            public override async Task<Dictionary<int, IProduct>> GetAllProductsAsync()
            {
                return await this.dataContext.GetAllProductsAsync();
            }


            public override async Task<Dictionary<int, IWarehouseEntry>> GetAllWarehouseEntriesAsync()
            {
                return await this.dataContext.GetAllWarehouseEntriesAsync();
            }



            public async override Task<ICustomer> GetCustomerAsync(int id)
            {
                IUser? user = await this.dataContext.GetCustomerAsync(id);

                if (user is null)
                    throw new Exception("This user does not exist!");

                return (ICustomer)user;
            }

            public async override Task<int> GetCustomerCountAsync()
            {
                return await this.dataContext.GetCustomersCountAsync();
            }

            public async override Task<IEventSold> GetEventAsync(int id)
            {
                IEventSold? even = await dataContext.GetEventAsync(id);

                if (even is null)
                    throw new Exception("This event does not exist!");

                return even;
            }

            public async override Task<int> GetEventsCountAsync()
            {
                return await dataContext.GetEventsCountAsync();
            }

            public async override Task<IProduct> GetProductAsync(int id)
            {
                IProduct? product = await dataContext.GetProductAsync(id);

                if (product is null)
                    throw new Exception("This product does not exist!");

                return product;
            }

            public override async Task<int> GetProductsCountAsync()
            {
                return await dataContext.GetProductsCountAsync();
            }



            public override async Task<int> GetWarehouseEntriesCountAsync()
            {
                return await dataContext.GetWarehouseEntriesCountAsync();
            }

            public async override Task<IWarehouseEntry> GetWarehouseEntryAsync(int id)
            {
                IWarehouseEntry? warehouseEntry = await dataContext.GetWarehouseEntryAsync(id);

                if (warehouseEntry is null)
                    throw new Exception("This warehouseEntry does not exist!");

                return warehouseEntry;
            }


            public async override Task UpdateCustomerAsync(int id, string name)
            {
                IUser user = new Customer(id, name);

                if (!await dataContext.DoesCustomerExist(user.Id))
                    throw new Exception("This user does not exist");

                await this.dataContext.UpdateCustomerAsync(user);
            }

            public async override Task UpdateEventAsync(int id, int stateId, int userId, DateTime date, int quantity)
            {
                IEventSold newEvent = new EventSold(id, stateId, userId, date, quantity);

                if (!await dataContext.DoesEventExists(newEvent.Id))
                    throw new Exception("This event does not exist");

                await dataContext.UpdateEventAsync(newEvent);
            }

            public async override Task UpdateProductAsync(int id, string name, decimal price)
            {
                IProduct product = new Product(id, name, price);

                if (!await dataContext.DoesProductExists(product.Id))
                    throw new Exception("This product does not exist");

                await dataContext.UpdateProductAsync(product);
            }

            public async override Task UpdateWarehouseEntryAsync(int id, int productId, int quantity)
            {
                IWarehouseEntry warehouseEntry = new WarehouseEntry(id, productId, quantity);

                if (!await dataContext.DoesWarehouseEntryExistsAsync(warehouseEntry.Id))
                    throw new Exception("This warehouse entry does not exist");

                await dataContext.UpdateWarehouseEntryAsync(warehouseEntry);
            }
        }
    }
}
