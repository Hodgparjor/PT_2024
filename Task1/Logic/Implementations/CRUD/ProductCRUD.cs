using Data;
using Data.Interfaces;
using Logic.API.CRUD;
using Logic.API.DTO;
using Logic.Implementations.DTO;


namespace Logic.Implementations.CRUD
{
    internal class ProductCRUD : IProductCRUD
    {
        private DataLayerAbstract _repository;

        public ProductCRUD(DataLayerAbstract repository)
        {
            this._repository = repository;
        }

        private IProductDTO ProductToDTO(IProduct product)
        {
            return new ProductDTO(product.Id, product.Name, product.Price);
        }

        public async Task AddProductAsync(int id, string name, decimal price)
        {
            await this._repository.AddProductAsync(id, name, price);
        }

        public async Task<IProductDTO> GetProductAsync(int id)
        {
            return this.ProductToDTO(await this._repository.GetProductAsync(id));
        }

        public async Task UpdateProductAsync(int id, string name, decimal price)
        {
            await this._repository.UpdateProductAsync(id, name, price);
        }

        public async Task DeleteProductAsync(int id)
        {
            await this._repository.DeleteProductAsync(id);
        }

        public async Task<Dictionary<int, IProductDTO>> GetAllProductsAsync()
        {
            Dictionary<int, IProductDTO> result = new Dictionary<int, IProductDTO>();

            foreach (IProduct product in (await this._repository.GetAllProductsAsync()).Values)
            {
                result.Add(product.Id, this.ProductToDTO(product));
            }

            return result;
        }

        public async Task<int> GetProductsCountAsync()
        {
            return await this._repository.GetProductsCountAsync();
        }
    }
}
