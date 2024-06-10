using Logic.API.CRUD;
using Logic.API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationTests.Mock
{
    internal class MockWarehouseEntryCRUD : IWarehouseEntryCRUD
    {
        private readonly MockRepository _mockRepository = new MockRepository();

        public MockWarehouseEntryCRUD()
        {

        }

        public async Task AddWarehouseEntryAsync(int id, int productId, int productQuantity)
        {
            await _mockRepository.AddWarehouseEntryAsync(id, productId, productQuantity);
        }

        public async Task<IWarehouseEntryDTO> GetWarehouseEntryAsync(int id)
        {
            return await this._mockRepository.GetWarehouseEntryAsync(id);
        }

        public async Task UpdateWarehouseEntryAsync(int id, int productId, int productQuantity)
        {
            await this._mockRepository.UpdateWarehouseEntryAsync(id, productId, productQuantity);
        }

        public async Task DeleteWarehouseEntryAsync(int id)
        {
            await this._mockRepository.DeleteWarehouseEntryAsync(id);
        }

        public async Task<Dictionary<int, IWarehouseEntryDTO>> GetAllWarehouseEntriesAsync()
        {
            Dictionary<int, IWarehouseEntryDTO> result = new Dictionary<int, IWarehouseEntryDTO>();

            foreach (IWarehouseEntryDTO warehouseEntry in (await this._mockRepository.GetAllWarehouseEntriesAsync()).Values)
            {
                result.Add(warehouseEntry.Id, warehouseEntry);
            }

            return result;
        }

        public async Task<int> GetWarehouseEntriesCountAsync()
        {
            return await this._mockRepository.GetWarehouseEntriesCountAsync();
        }
    }
}
