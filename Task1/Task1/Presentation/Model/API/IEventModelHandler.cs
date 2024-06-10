using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.API;
using Logic.API.CRUD;

namespace Presentation.Model.API
{
    internal interface IEventModelHandler
    {
        static IEventModelHandler CreateModelOperation(IEventCRUD? eventCrud = null)
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
