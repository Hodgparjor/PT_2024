using Data;
using Logic.API.DTO;
using Logic.Implementations.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.API.CRUD
{
    internal interface IUserCRUD
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
