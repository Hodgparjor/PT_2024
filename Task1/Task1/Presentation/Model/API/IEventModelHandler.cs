using Logic.API.CRUD;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Model.API
{
    public interface IEventModelHandler
    {
        static IEventModelHandler CreateModelHandler(IEventCRUD? eventCrud = null)
        {
            return new EventModelHandler(eventCrud ?? IEventCRUD.CreateEventCRUD());
        }

        Task AddAsync(int id, int stateId, int userId, int quantity);

        Task<IEventModel> GetAsync(int id);

        Task UpdateAsync(int id, int stateId, int userId, DateTime date, int quantity);

        Task DeleteAsync(int id);

        Task<Dictionary<int, IEventModel>> GetAllAsync();

        Task<int> GetCountAsync();
    }
}
