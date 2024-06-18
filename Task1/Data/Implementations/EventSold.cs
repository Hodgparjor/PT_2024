using Data.Interfaces;

namespace Data
{
    internal class EventSold : Event, IEventSold
    {
        private Customer _customer;
        private Product _soldProduct;
        private int _quantity;
        private int _warehouseEntryId;

        private int _customerId;
        private int _productId;
        private DateTime _date;
        private int _id;

        public EventSold(Customer customer, Product product, int quantity)
        {
            Date = DateTime.Now;
            _customer = customer;
            _soldProduct = product;
            _quantity = quantity;
            _customer.BoughtProducts.Add(new WarehouseEntry(product, quantity));
        }

        public EventSold(int id, int customerId, int warehouseEntryId, DateTime date, int quantity)
        {
            _id = id;
            _customerId = customerId;
            _quantity = quantity;
            _date = date;
            _warehouseEntryId = warehouseEntryId;
            _productId = warehouseEntryId;
        }

        public int Quantity { get => _quantity; set => _quantity = value; }
        public int CustomerId { get => _customerId; set => _customerId = value; }
        public int ProductId { get => _productId; set => _productId = value; }
       
        public DateTime Date { get => _date; set => _date = value; }
        public int Id { get => _id; set => _id = value; }
        public int WarehouseEntryId { get => _warehouseEntryId; set => _warehouseEntryId = value; }
        internal Customer Customer { get => _customer; set => _customer = value; }
        internal Product SoldProduct { get => _soldProduct; set => _soldProduct = value; }
    }
}
