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
    internal class ProductModelHandler : IProductModelHandler
    {
        private IProductCRUD _productCRUD;

        public ProductModelHandler(IProductCRUD? productCrud = null)
        {
            this._productCRUD = productCrud ?? IProductCRUD.CreateProductCRUD();
        }

        private IProductModel Map(IProductDTO product)
        {
            return new ProductModel(product.Id, product.Name, product.Price);
        }

        public async Task AddAsync(int id, string name, decimal price)
        {
            await this._productCRUD.AddProductAsync(id, name, price);
        }

        public async Task<IProductModel> GetAsync(int id)
        {
            return this.Map(await this._productCRUD.GetProductAsync(id));
        }

        public async Task UpdateAsync(int id, string name, decimal price)
        {
            await this._productCRUD.UpdateProductAsync(id, name, price);
        }

        public async Task DeleteAsync(int id)
        {
            await this._productCRUD.DeleteProductAsync(id);
        }

        public async Task<Dictionary<int, IProductModel>> GetAllAsync()
        {
            Dictionary<int, IProductModel> result = new Dictionary<int, IProductModel>();

            foreach (IProductDTO product in (await this._productCRUD.GetAllProductsAsync()).Values)
            {
                result.Add(product.Id, this.Map(product));
            }

            return result;
        }

        public async Task<int> GetCountAsync()
        {
            return await this._productCRUD.GetProductsCountAsync();
        }
    }
}
