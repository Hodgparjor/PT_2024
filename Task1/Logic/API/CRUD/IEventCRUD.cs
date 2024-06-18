using Data;
using Logic.API.DTO;
using Logic.Implementations.CRUD;

namespace Logic.API.CRUD
{
    public interface IEventCRUD
    {
        static IEventCRUD CreateEventCRUD(DataLayerAbstract? dataRepository = null)
        {
            return new EventCRUD(dataRepository ?? DataLayerAbstract.CreateDatabase());
        }

        Task AddEventAsync(int id, int stateId, int userId, int quantity = 0);

        Task<IEventDTO> GetEventAsync(int id);

        Task UpdateEventAsync(int id, int stateId, int userId, DateTime occurrenceDate, int quantity);

        Task DeleteEventAsync(int id);

        Task<Dictionary<int, IEventDTO>> GetAllEventsAsync();

        Task<int> GetEventsCountAsync();
    }
}
