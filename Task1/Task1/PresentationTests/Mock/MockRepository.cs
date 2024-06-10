using Logic.API.CRUD;
using Logic.API.DTO;

namespace PresentationTests.Mock
{
    internal class MockRepository
    {
        public Dictionary<int, IUserDTO> Users = new Dictionary<int, IUserDTO>();

        public Dictionary<int, IProductDTO> Products = new Dictionary<int, IProductDTO>();

        public Dictionary<int, IEventDTO> Events = new Dictionary<int, IEventDTO>();

        public Dictionary<int, IWarehouseEntryDTO> States = new Dictionary<int, IWarehouseEntryDTO>();

        #region User CRUD

        public async Task AddUserAsync(int id, string name)
        {
            this.Users.Add(id, new MockUserDTO(id, name));
        }

        public async Task<IUserDTO> GetUserAsync(int id)
        {
            return await Task.FromResult(this.Users[id]);
        }

        public async Task UpdateUserAsync(int id, string name)
        {
            this.Users[id].Name = name;
        }

        public async Task DeleteUserAsync(int id)
        {
            this.Users.Remove(id);
        }

        public async Task<Dictionary<int, IUserDTO>> GetAllUsersAsync()
        {
            return await Task.FromResult(this.Users);
        }

        public async Task<int> GetUsersCountAsync()
        {
            return await Task.FromResult(this.Users.Count);
        }

        public bool CheckIfUserExists(int id)
        {
            return this.Users.ContainsKey(id);
        }

        #endregion User CRUD


        #region Product CRUD

        public async Task AddProductAsync(int id, string name, decimal price)
        {
            this.Products.Add(id, new MockProductDTO(id, name, price));
        }

        public async Task<IProductDTO> GetProductAsync(int id)
        {
            return await Task.FromResult(this.Products[id]);
        }

        public async Task UpdateProductAsync(int id, string name, decimal price)
        {
            this.Products[id].Name = name;
            this.Products[id].Price = price;
        }

        public async Task DeleteProductAsync(int id)
        {
            this.Products.Remove(id);
        }

        public async Task<Dictionary<int, IProductDTO>> GetAllProductsAsync()
        {
            return await Task.FromResult(this.Products);
        }

        public async Task<int> GetProductsCountAsync()
        {
            return await Task.FromResult(this.Products.Count);
        }

        #endregion


        #region State CRUD

        public async Task AddWarehouseEntryAsync(int id, int productId, int quantity)
        {
            this.States.Add(id, new MockWarehouseEntryDTO(id, productId, quantity));
        }

        public async Task<IWarehouseEntryDTO> GetWarehouseEntryAsync(int id)
        {
            return await Task.FromResult(this.States[id]);
        }

        public async Task UpdateWarehouseEntryAsync(int id, int productId, int quantity)
        {
            this.States[id].productId = productId;
            this.States[id].quantity = quantity;
        }

        public async Task DeleteWarehouseEntryAsync(int id)
        {
            this.States.Remove(id);
        }

        public async Task<Dictionary<int, IWarehouseEntryDTO>> GetAllWarehouseEntriesAsync()
        {
            return await Task.FromResult(this.States);
        }

        public async Task<int> GetWarehouseEntriesCountAsync()
        {
            return await Task.FromResult(this.States.Count);
        }

        #endregion


        #region Event CRUD

        public async Task AddEventAsync(int id, int stateId, int userId, int quantity)
        {
            IUserDTO user = await this.GetUserAsync(userId);
            IWarehouseEntryDTO warehouseEntry = await this.GetWarehouseEntryAsync(stateId);
            IProductDTO product = await this.GetProductAsync(warehouseEntry.productId);

            if (warehouseEntry.quantity == 0)
                throw new Exception("Product unavailable, please check later!");


            await this.UpdateWarehouseEntryAsync(stateId, product.Id, warehouseEntry.quantity - 1);

            this.Events.Add(id, new MockEventDTO(id, stateId, userId, quantity));
        }

        public async Task<IEventDTO> GetEventAsync(int id)
        {
            return await Task.FromResult(this.Events[id]);
        }

        public async Task UpdateEventAsync(int id, int stateId, int userId, DateTime occurrenceDate, int quantity)
        {
            ((MockEventDTO)this.Events[id]).ProductId = stateId;
            ((MockEventDTO)this.Events[id]).UserId = userId;
            ((MockEventDTO)this.Events[id]).Date = occurrenceDate;
            ((MockEventDTO)this.Events[id]).Quantity = quantity;
        }

        public async Task DeleteEventAsync(int id)
        {
            this.Events.Remove(id);
        }

        public async Task<Dictionary<int, IEventDTO>> GetAllEventsAsync()
        {
            return await Task.FromResult(this.Events);
        }

        public async Task<int> GetEventsCountAsync()
        {
            return await Task.FromResult(this.Events.Count);
        }

        #endregion
    }
}
