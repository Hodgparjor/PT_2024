using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class EventSold : Event
    {
        private Customer _customer;
        private Product _soldProduct;
        private int _quantity;

        public EventSold(Customer customer, Product product, int quantity)
        {
            Date = DateTime.Now;
            _customer = customer;
            _soldProduct = product;
            _quantity = quantity;
        }

        public int Quantity { get => _quantity; set => _quantity = value; }
        internal Customer Customer { get => _customer; set => _customer = value; }
        internal Product SoldProduct { get => _soldProduct; set => _soldProduct = value; }
    }
}
