using Logic.API.CRUD;
using Logic.API.DTO;


namespace PresentationTests.Mock
{
    internal class MockProductCRUD : IProductCRUD
    {
        private readonly MockRepository _mockRepository = new MockRepository();

        public MockProductCRUD()
        {

        }

        public async Task AddProductAsync(int id, string name, decimal price)
        {
            await this._mockRepository.AddProductAsync(id, name, price);
        }

        public async Task<IProductDTO> GetProductAsync(int id)
        {
            return await this._mockRepository.GetProductAsync(id);
        }

        public async Task UpdateProductAsync(int id, string name, decimal price)
        {
            await this._mockRepository.UpdateProductAsync(id, name, price);
        }

        public async Task DeleteProductAsync(int id)
        {
            await this._mockRepository.DeleteProductAsync(id);
        }

        public async Task<Dictionary<int, IProductDTO>> GetAllProductsAsync()
        {
            Dictionary<int, IProductDTO> result = new Dictionary<int, IProductDTO>();

            foreach (IProductDTO product in (await this._mockRepository.GetAllProductsAsync()).Values)
            {
                result.Add(product.Id, product);
            }

            return result;
        }

        public async Task<int> GetProductsCountAsync()
        {
            return await this._mockRepository.GetProductsCountAsync();
        }
    }
}
