using Logic.API.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model.API
{
    internal interface IProductModelHandler
    {
        static IProductModelHandler CreateModelHandler(IProductCRUD? productCrud = null)
        {
            return new ProductModelHandler(productCrud ?? IProductCRUD.CreateProductCRUD());
        }

        Task AddAsync(int id, string name, decimal price);

        Task<IProductModel> GetAsync(int id);

        Task UpdateAsync(int id, string name, decimal price);

        Task DeleteAsync(int id);

        Task<Dictionary<int, IProductModel>> GetAllAsync();

        Task<int> GetCountAsync();
    }
}
