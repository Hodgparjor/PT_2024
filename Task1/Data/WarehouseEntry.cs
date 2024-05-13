using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class WarehouseEntry : IWarehouseEntry
    {
        private Product _product;
        private int _quantity;
        private int _id;

        public WarehouseEntry(Product product, int quantity)
        {
            _product = product;
            _id = product.Id;
            _quantity = quantity;
        }

        public int Quantity { get => _quantity; set => _quantity = value; }
        public int Id { get => _id; set => _id = value; }
        public IProduct Product { get => _product; set => _product = (Product)value; }
    }
}
