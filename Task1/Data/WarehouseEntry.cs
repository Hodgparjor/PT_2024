using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class WarehouseEntry
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
        internal Product Product { get => _product; set => _product = value; }
    }
}
