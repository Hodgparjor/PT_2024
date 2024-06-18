

namespace Data
{
    internal class EventDelivery : Event
    {
        private Supplier _supplier;
        private Product _deliveredProduct;
        private int _quantity;

        public EventDelivery(Supplier supplier, Product deliveredProduct, int quantity)
        {
            _supplier = supplier;
            _deliveredProduct = deliveredProduct;
            Date = DateTime.Now;
            _quantity = quantity;
            supplier.SuppliedProducts.Add(new WarehouseEntry(deliveredProduct, quantity));
        }

        internal Product DeliveredProduct { get => _deliveredProduct; set => _deliveredProduct = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }
        public Supplier Supplier { get => _supplier; set => _supplier = value; }
    }
}
