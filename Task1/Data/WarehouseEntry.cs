using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class WarehouseEntry
    {
        private Product product;
        private int quantity;

        public int Quantity { get => quantity; set => quantity = value; }
        internal Product Product { get => product; set => product = value; }
    }
}
