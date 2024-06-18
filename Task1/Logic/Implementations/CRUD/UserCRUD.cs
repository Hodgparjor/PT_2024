using Data;
using Data.Interfaces;
using Logic.API.CRUD;
using Logic.API.DTO;
using Logic.Implementations.DTO;

namespace Logic.Implementations.CRUD
{
    internal class UserCRUD : IUserCRUD
    {
        private DataLayerAbstract _repository;

        public UserCRUD(DataLayerAbstract repository)
        {
            this._repository = repository;
        }

        private IUserDTO UserToDTO(IUser user)
        {
            return new UserDTO(user.Id, user.Name);
        }

        public async Task AddUserAsync(int id, string name)
        {
            await this._repository.AddCustomerAsync(id, name);
        }

        public async Task<IUserDTO> GetUserAsync(int id)
        {
            return this.UserToDTO(await this._repository.GetCustomerAsync(id));
        }

        public async Task UpdateUserAsync(int id, string name)
        {
            await this._repository.UpdateCustomerAsync(id, name);
        }

        public async Task DeleteUserAsync(int id)
        {
            await this._repository.DeleteCustomerAsync(id);
        }

        public async Task<Dictionary<int, IUserDTO>> GetAllUsersAsync()
        {
            Dictionary<int, IUserDTO> result = new Dictionary<int, IUserDTO>();

            foreach (IUser user in (await this._repository.GetAllCustomersAsync()).Values)
            {
                result.Add(user.Id, this.UserToDTO(user));
            }

            return result;
        }

        public async Task<int> GetUsersCountAsync()
        {
            return await this._repository.GetCustomerCountAsync();
        }
    }
}
