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
    internal interface IProductCRUD
    {
        static IProductCRUD CreateProductCRUD(DataLayerAbstract? dataRepository = null)
        {
            return new ProductCRUD(dataRepository ?? DataLayerAbstract.CreateDatabase());
        }

        Task AddProductAsync(int id, string name, decimal price);

        Task<IProductDTO> GetProductAsync(int id);

        Task UpdateProductAsync(int id, string name, decimal price);

        Task DeleteProductAsync(int id);

        Task<Dictionary<int, IProductDTO>> GetAllProductsAsync();

        Task<int> GetProductsCountAsync();
    }
}
