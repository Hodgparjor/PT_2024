using Logic.API.CRUD;
using Logic.API.DTO;

namespace PresentationTests.Mock
{
    internal class MockUserCRUD : IUserCRUD
    {
        private readonly MockRepository _mockRepository = new MockRepository();

        public MockUserCRUD()
        {

        }

        public async Task AddUserAsync(int id, string name)
        {
            await this._mockRepository.AddUserAsync(id, name);
        }

        public async Task<IUserDTO> GetUserAsync(int id)
        {
            return await this._mockRepository.GetUserAsync(id);
        }

        public async Task UpdateUserAsync(int id, string name)
        {
            await this._mockRepository.UpdateUserAsync(id, name);
        }

        public async Task DeleteUserAsync(int id)
        {
            await this._mockRepository.DeleteUserAsync(id);
        }

        public async Task<Dictionary<int, IUserDTO>> GetAllUsersAsync()
        {
            Dictionary<int, IUserDTO> result = new Dictionary<int, IUserDTO>();

            foreach (IUserDTO user in (await this._mockRepository.GetAllUsersAsync()).Values)
            {
                result.Add(user.Id, user);
            }

            return result;
        }

        public async Task<int> GetUsersCountAsync()
        {
            return await this._mockRepository.GetUsersCountAsync();
        }
    }
}
