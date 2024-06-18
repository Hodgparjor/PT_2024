using Data;
using Data.Interfaces;
using Logic.API.CRUD;
using Logic.API.DTO;
using Logic.Implementations.DTO;


namespace Logic.Implementations.CRUD
{
    internal class EventCRUD : IEventCRUD
    {
        private DataLayerAbstract _repository;

        public EventCRUD(DataLayerAbstract repository)
        {
            _repository = repository;
        }

        public IEventDTO EventToDTO(IEventSold eventSold)
        {
            return new EventDTO(eventSold.Id, eventSold.ProductId, eventSold.CustomerId, eventSold.Date, eventSold.Quantity);
        }

        public async Task AddEventAsync(int id, int productId, int userId, int quantity = 0)
        {
            await _repository.AddEventAsync(id, productId, userId, quantity);
        }

        public async Task<IEventDTO> GetEventAsync(int id)
        {
            return this.EventToDTO(await _repository.GetEventAsync(id));
        }

        public async Task UpdateEventAsync(int id, int productId, int userId, DateTime date, int quantity)
        {
            await _repository.UpdateEventAsync(id, productId, userId, date, quantity);
        }

        public async Task DeleteEventAsync(int id)
        {
            await _repository.DeleteEventAsync(id);
        }

        public async Task<Dictionary<int, IEventDTO>> GetAllEventsAsync()
        {
            Dictionary<int, IEventDTO> result = new Dictionary<int, IEventDTO>();

            foreach (IEventSold even in (await _repository.GetAllEventsAsync()).Values)
            {
                result.Add(even.Id, EventToDTO(even));
            }

            return result;
        }

        public async Task<int> GetEventsCountAsync()
        {
            return await _repository.GetEventsCountAsync();
        }
    }
}
