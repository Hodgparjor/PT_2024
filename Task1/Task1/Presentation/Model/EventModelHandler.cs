using Logic.API.CRUD;
using Logic.API.DTO;
using Presentation.Model.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    internal class EventModelHandler : IEventModelHandler
    {
        private IEventCRUD _eventCRUD;

        public EventModelHandler(IEventCRUD? eventCrud = null)
        {
            this._eventCRUD = eventCrud ?? IEventCRUD.CreateEventCRUD();
        }

        private IEventModel Map(IEventDTO even)
        {
            return new EventModel(even.Id, even.ProductId, even.UserId, even.Quantity, even.Date);
        }

        public async Task AddAsync(int id, int stateId, int userId, int quantity)
        {
            await this._eventCRUD.AddEventAsync(id, stateId, userId, quantity);
        }

        public async Task<IEventModel> GetAsync(int id)
        {
            return this.Map(await this._eventCRUD.GetEventAsync(id));
        }

        public async Task UpdateAsync(int id, int stateId, int userId, DateTime occurrenceDate, int quantity)
        {
            await this._eventCRUD.UpdateEventAsync(id, stateId, userId, occurrenceDate, quantity);
        }

        public async Task DeleteAsync(int id)
        {
            await this._eventCRUD.DeleteEventAsync(id);
        }

        public async Task<Dictionary<int, IEventModel>> GetAllAsync()
        {
            Dictionary<int, IEventModel> result = new Dictionary<int, IEventModel>();

            foreach (IEventDTO even in (await this._eventCRUD.GetAllEventsAsync()).Values)
            {
                result.Add(even.Id, this.Map(even));
            }

            return result;
        }

        public async Task<int> GetCountAsync()
        {
            return await this._eventCRUD.GetEventsCountAsync();
        }
    }
}
