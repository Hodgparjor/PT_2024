using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.API.CRUD;
using Logic.API.DTO;
using Presentation.Model.API;

namespace Presentation.Model
{
    internal class WarehouseEntryModelHandler : IWarehouseEntryModelHandler
    {
        private IWarehouseEntryCRUD _warehouseEntryCrud;

        public WarehouseEntryModelHandler(IWarehouseEntryCRUD? stateCrud = null)
        {
            this._warehouseEntryCrud = stateCrud ?? IWarehouseEntryCRUD.CreateStateCRUD();
        }

        private IWarehouseEntryModel Map(IWarehouseEntryDTO state)
        {
            return new WarehouseEntryModel(state.Id, state.productId, state.quantity);
        }

        public async Task AddAsync(int id, int productId, int productQuantity)
        {
            await this._warehouseEntryCrud.AddWarehouseEntryAsync(id, productId, productQuantity);
        }

        public async Task<IWarehouseEntryModel> GetAsync(int id)
        {
            return this.Map(await this._warehouseEntryCrud.GetWarehouseEntryAsync(id));
        }

        public async Task UpdateAsync(int id, int productId, int productQuantity)
        {
            await this._warehouseEntryCrud.UpdateWarehouseEntryAsync(id, productId, productQuantity);
        }

        public async Task DeleteAsync(int id)
        {
            await this._warehouseEntryCrud.DeleteWarehouseEntryAsync(id);
        }

        public async Task<Dictionary<int, IWarehouseEntryModel>> GetAllAsync()
        {
            Dictionary<int, IWarehouseEntryModel> result = new Dictionary<int, IWarehouseEntryModel>();

            foreach (IWarehouseEntryDTO state in (await this._warehouseEntryCrud.GetAllWarehouseEntriesAsync()).Values)
            {
                result.Add(state.Id, this.Map(state));
            }

            return result;
        }

        public async Task<int> GetCountAsync()
        {
            return await this._warehouseEntryCrud.GetWarehouseEntriesCountAsync();
        }
    }
}
