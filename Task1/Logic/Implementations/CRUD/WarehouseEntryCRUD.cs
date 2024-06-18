using Data;
using Data.Interfaces;
using Logic.API.CRUD;
using Logic.API.DTO;
using Logic.Implementations.DTO;


namespace Logic.Implementations.CRUD
{
    internal class WarehouseEntryCRUD : IWarehouseEntryCRUD
    {
        private DataLayerAbstract _repository;

        public WarehouseEntryCRUD(DataLayerAbstract repository)
        {
            this._repository = repository;
        }

        private IWarehouseEntryDTO WarehouseEntryToDTO(IWarehouseEntry entry)
        {
            return new WarehouseEntryDTO(entry.Id, entry.ProductId, entry.Quantity);
        }

        public async Task AddWarehouseEntryAsync(int id, int productId, int quantity)
        {
            await _repository.AddWarehouseEntryAsync(id, productId, quantity);
        }

        public async Task<IWarehouseEntryDTO> GetWarehouseEntryAsync(int id)
        {
            return this.WarehouseEntryToDTO(await this._repository.GetWarehouseEntryAsync(id));
        }

        public async Task UpdateWarehouseEntryAsync(int id, int productId, int quantity)
        {
            await this._repository.UpdateWarehouseEntryAsync(id, productId, quantity);
        }

        public async Task DeleteWarehouseEntryAsync(int id)
        {
            await this._repository.DeleteWarehouseEntryAsync(id);
        }

        public async Task<Dictionary<int, IWarehouseEntryDTO>> GetAllWarehouseEntriesAsync()
        {
            Dictionary<int, IWarehouseEntryDTO> result = new Dictionary<int, IWarehouseEntryDTO>();

            foreach (IWarehouseEntry entry in (await this._repository.GetAllWarehouseEntriesAsync()).Values)
            {
                result.Add(entry.Id, this.WarehouseEntryToDTO(entry));
            }

            return result;
        }

        public async Task<int> GetWarehouseEntriesCountAsync()
        {
            return await this._repository.GetWarehouseEntriesCountAsync();
        }
    }
}
