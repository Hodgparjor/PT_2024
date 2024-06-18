using Data;
using Logic.API.DTO;
using Logic.Implementations.CRUD;

namespace Logic.API.CRUD
{
    public interface IProductCRUD
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
