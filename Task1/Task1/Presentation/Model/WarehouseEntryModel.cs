using Presentation.Model.API;

namespace Presentation.Model
{
    internal class WarehouseEntryModel : IWarehouseEntryModel
    {
        public WarehouseEntryModel(int id, int productId, int quantity) {
            Id = id;
            this.productId = productId;
            this.quantity = quantity;
        }
        public int Id { get; set; }
        public int productId { get; set; }
        public int quantity { get; set; }
    }
}
