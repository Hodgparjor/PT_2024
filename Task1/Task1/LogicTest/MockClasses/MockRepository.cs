using Data;
using Data.Interfaces;

namespace LogicTest.MockClasses
{
    internal class MockRepository : DataLayerAbstract
    {
        public Dictionary<int, ICustomer> Users = new Dictionary<int, ICustomer>();

        public Dictionary<int, IProduct> Products = new Dictionary<int, IProduct>();

        public Dictionary<int, IEventSold> Events = new Dictionary<int, IEventSold>();

        public Dictionary<int, IWarehouseEntry> WarehouseEntries = new Dictionary<int, IWarehouseEntry>();

        #region User CRUD

        public override async Task AddCustomerAsync(int id, string name)
        {
            this.Users.Add(id, (ICustomer)new MockUser(id, name));
        }

        public override async Task<ICustomer> GetCustomerAsync(int id)
        {
            return await Task.FromResult(this.Users[id]);
        }

        public override async Task UpdateCustomerAsync(int id, string name)
        {
            this.Users[id].Name = name;
        }

        public override async Task DeleteCustomerAsync(int id)
        {
            this.Users.Remove(id);
        }

        public override async Task<Dictionary<int, ICustomer>> GetAllCustomersAsync()
        {
            return await Task.FromResult(this.Users);
        }

        public override async Task<int> GetCustomerCountAsync()
        {
            return await Task.FromResult(this.Users.Count);
        }

        public bool CheckIfCustomerExists(int id)
        {
            return this.Users.ContainsKey(id);
        }

        #endregion User CRUD


        #region Product CRUD

        public override async Task AddProductAsync(int id, string name, decimal price)
        {
            this.Products.Add(id, new MockProduct(id, name, price));
        }

        public override async Task<IProduct> GetProductAsync(int id)
        {
            return await Task.FromResult(this.Products[id]);
        }

        public override async Task UpdateProductAsync(int id, string name, decimal price)
        {
            this.Products[id].Name = name;
            this.Products[id].Price = price;
        }

        public override async Task DeleteProductAsync(int id)
        {
            this.Products.Remove(id);
        }

        public override async Task<Dictionary<int, IProduct>> GetAllProductsAsync()
        {
            return await Task.FromResult(this.Products);
        }

        public override async Task<int> GetProductsCountAsync()
        {
            return await Task.FromResult(this.Products.Count);
        }

        #endregion


        #region State CRUD

        public override async Task AddWarehouseEntryAsync(int id, int productId, int quantity)
        {
            this.WarehouseEntries.Add(id, new MockWarehouseEntry(id, productId, quantity));
        }

        public override async Task<IWarehouseEntry> GetWarehouseEntryAsync(int id)
        {
            return await Task.FromResult(this.WarehouseEntries[id]);
        }

        public override async Task UpdateWarehouseEntryAsync(int id, int productId, int quantity)
        {
            this.WarehouseEntries[id].ProductId = productId;
            this.WarehouseEntries[id].Quantity = quantity;
        }

        public override async Task DeleteWarehouseEntryAsync(int id)
        {
            this.WarehouseEntries.Remove(id);
        }

        public override async Task<Dictionary<int, IWarehouseEntry>> GetAllWarehouseEntriesAsync()
        {
            return await Task.FromResult(this.WarehouseEntries);
        }

        public override async Task<int> GetWarehouseEntriesCountAsync()
        {
            return await Task.FromResult(this.WarehouseEntries.Count);
        }

        #endregion


        #region Event CRUD

        public override async Task AddEventAsync(int id, int stateId, int userId, int quantity)
        {
            IUser user = await this.GetCustomerAsync(userId);
            IWarehouseEntry warehouseEntry = await this.GetWarehouseEntryAsync(stateId);
            IProduct product = await this.GetProductAsync(warehouseEntry.ProductId);

            if (warehouseEntry.Quantity == 0)
                throw new Exception("Product unavailable, please check later!");


            await this.UpdateWarehouseEntryAsync(stateId, product.Id, warehouseEntry.Quantity - quantity);

            this.Events.Add(id, (IEventSold)new MockEvent(id, stateId, userId, quantity));
        }

        public override async Task<IEventSold> GetEventAsync(int id)
        {
            return await Task.FromResult(this.Events[id]);
        }

        public override async Task UpdateEventAsync(int id, int stateId, int userId, DateTime occurrenceDate, int quantity)
        {
            ((MockEvent)this.Events[id]).ProductId = stateId;
            ((MockEvent)this.Events[id]).UserId = userId;
            ((MockEvent)this.Events[id]).Date = occurrenceDate;
            ((MockEvent)this.Events[id]).Quantity = quantity;
        }

        public override async Task DeleteEventAsync(int id)
        {
            this.Events.Remove(id);
        }

        public override async Task<Dictionary<int, IEventSold>> GetAllEventsAsync()
        {
            return await Task.FromResult(this.Events);
        }

        public override async Task<int> GetEventsCountAsync()
        {
            return await Task.FromResult(this.Events.Count);
        }

        #endregion
    }
}
