using Data.Interfaces;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicTest.MockClasses
{
    internal class MockWarehouseEntry : IWarehouseEntry
    {
        private MockProduct _product;
        private int _quantity;
        private int _id;
        private int _productId;

        public MockWarehouseEntry(MockProduct product, int quantity)
        {
            _product = product;
            _id = product.Id;
            _productId = product.Id;
            _quantity = quantity;
        }

        public MockWarehouseEntry(int id, int productId, int quantity)
        {
            _productId = productId;
            _id = id;
            _quantity = quantity;
        }

        public int Quantity { get => _quantity; set => _quantity = value; }
        public int Id { get => _id; set => _id = value; }
        public IProduct Product { get => _product; set => _product = (MockProduct)value; }
        public int ProductId { get => _productId; set => _productId = value; }
    }
}
