using Logic.API.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model.API
{
    public interface IWarehouseEntryModelHandler
    {
        static IWarehouseEntryModelHandler CreateModelHandler(IWarehouseEntryCRUD? stateCrud = null)
        {
            return new WarehouseEntryModelHandler(stateCrud ?? IWarehouseEntryCRUD.CreateStateCRUD());
        }

        Task AddAsync(int id, int productId, int productQuantity);

        Task<IWarehouseEntryModel> GetAsync(int id);

        Task UpdateAsync(int id, int productId, int productQuantity);

        Task DeleteAsync(int id);

        Task<Dictionary<int, IWarehouseEntryModel>> GetAllAsync();

        Task<int> GetCountAsync();
    }
}
