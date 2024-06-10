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
    internal class UserModelHandler : IUserModelHandler
    {
        private IUserCRUD _userCRUD;

        public UserModelHandler(IUserCRUD? userCrud)
        {
            this._userCRUD = userCrud ?? IUserCRUD.CreateUserCRUD();
        }

        private IUserModel Map(IUserDTO user)
        {
            return new UserModel(user.Id, user.Name);
        }

        public async Task AddAsync(int id, string name)
        {
            await this._userCRUD.AddUserAsync(id, name);
        }

        public async Task<IUserModel> GetAsync(int id)
        {
            return this.Map(await this._userCRUD.GetUserAsync(id));
        }

        public async Task UpdateAsync(int id, string name)
        {
            await this._userCRUD.UpdateUserAsync(id, name);
        }

        public async Task DeleteAsync(int id)
        {
            await this._userCRUD.DeleteUserAsync(id);
        }

        public async Task<Dictionary<int, IUserModel>> GetAllAsync()
        {
            Dictionary<int, IUserModel> result = new Dictionary<int, IUserModel>();

            foreach (IUserDTO user in (await this._userCRUD.GetAllUsersAsync()).Values)
            {
                result.Add(user.Id, this.Map(user));
            }

            return result;
        }

        public async Task<int> GetCountAsync()
        {
            return await this._userCRUD.GetUsersCountAsync();
        }
    }
}
