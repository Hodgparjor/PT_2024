using Logic.API.CRUD;
using Logic.API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationTests.Mock
{
    internal class MockEventCRUD : IEventCRUD
    {
        private readonly MockRepository _mockRepository = new MockRepository();

        public MockEventCRUD()
        {

        }

        public async Task AddEventAsync(int id, int stateId, int userId, int quantity )
        {
            await this._mockRepository.AddEventAsync(id, stateId, userId, quantity);
        }

        public async Task<IEventDTO> GetEventAsync(int id)
        {
            return await this._mockRepository.GetEventAsync(id);
        }

        public async Task UpdateEventAsync(int id, int stateId, int userId, DateTime occurrenceDate, int quantity)
        {
            await this._mockRepository.UpdateEventAsync(id, stateId, userId, occurrenceDate, quantity);
        }

        public async Task DeleteEventAsync(int id)
        {
            await this._mockRepository.DeleteEventAsync(id);
        }

        public async Task<Dictionary<int, IEventDTO>> GetAllEventsAsync()
        {
            Dictionary<int, IEventDTO> result = new Dictionary<int, IEventDTO>();

            foreach (IEventDTO even in (await this._mockRepository.GetAllEventsAsync()).Values)
            {
                result.Add(even.Id, even);
            }

            return result;
        }

        public async Task<int> GetEventsCountAsync()
        {
            return await this._mockRepository.GetEventsCountAsync();
        }
    }
}
