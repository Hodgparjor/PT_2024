using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class EventDelivery : Event
    {
        private Product _deliveredProduct;
        private int _quantity;

        public EventDelivery(Product deliveredProduct, int quantity)
        {
            _deliveredProduct = deliveredProduct;
            Date = DateTime.Now;
            _quantity = quantity;
        }

        internal Product DeliveredProduct { get => _deliveredProduct; set => _deliveredProduct = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }
    }
}
