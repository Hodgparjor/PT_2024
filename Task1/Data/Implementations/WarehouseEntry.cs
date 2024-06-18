using Data.Interfaces;

namespace Data
{
    internal class WarehouseEntry : IWarehouseEntry
    {
        private Product _product;
        private int _productId;
        private int _quantity;
        private int _id;

        public WarehouseEntry(Product product, int quantity)
        {
            _product = product;
            _id = product.Id;
            _productId = product.Id;
            _quantity = quantity;
        }

        public WarehouseEntry(int id, int productId, int quantity)
        {
            _id = id;
            _productId = productId;
            _quantity = quantity;
        }

        public int Quantity { get => _quantity; set => _quantity = value; }
        public int Id { get => _id; set => _id = value; }
        public int ProductId { get => _productId; set => _productId = value; }
        public IProduct Product { get => _product; set => _product = (Product)value; }
    }
}
