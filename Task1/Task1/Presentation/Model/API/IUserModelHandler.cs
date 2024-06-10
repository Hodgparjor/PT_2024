using Logic.API.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model.API
{
    public interface IUserModelHandler
    {
        static IUserModelHandler CreateModelHandler(IUserCRUD? userCrud = null)
        {
            return new UserModelHandler(userCrud);
        }

        Task AddAsync(int id, string name);

        Task<IUserModel> GetAsync(int id);

        Task UpdateAsync(int id, string name);

        Task DeleteAsync(int id);

        Task<Dictionary<int, IUserModel>> GetAllAsync();

        Task<int> GetCountAsync();
    }
}
