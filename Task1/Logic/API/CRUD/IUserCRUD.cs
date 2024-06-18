using Data;
using Logic.API.DTO;
using Logic.Implementations.CRUD;


namespace Logic.API.CRUD
{
    public interface IUserCRUD
    {
        static IUserCRUD CreateUserCRUD(DataLayerAbstract? dataRepository = null)
        {
            return new UserCRUD(dataRepository ?? DataLayerAbstract.CreateDatabase());
        }

        Task AddUserAsync(int id, string name);

        Task<IUserDTO> GetUserAsync(int id);

        Task UpdateUserAsync(int id, string name);

        Task DeleteUserAsync(int id);

        Task<Dictionary<int, IUserDTO>> GetAllUsersAsync();

        Task<int> GetUsersCountAsync();
    }
}
