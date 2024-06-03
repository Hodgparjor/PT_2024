using Data;
using Logic.API.DTO;
using Logic.Implementations.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.API.CRUD
{
    internal interface IWarehouseEntryCRUD
    {
        static IWarehouseEntryCRUD CreateStateCRUD(DataLayerAbstract? dataRepository = null)
        {
            return new WarehouseEntryCRUD(dataRepository ?? DataLayerAbstract.CreateDatabase());
        }

        Task AddWarehouseEntryAsync(int id, int productId, int quantity);

        Task<IWarehouseEntryDTO> GetWarehouseEntryAsync(int id);

        Task UpdateWarehouseEntryAsync(int id, int productId, int quantity);

        Task DeleteWarehouseEntryAsync(int id);

        Task<Dictionary<int, IWarehouseEntryDTO>> GetAllWarehouseEntriesAsync();

        Task<int> GetWarehouseEntriesCountAsync();
    }
}
