using Logic.API.DTO;

namespace Logic.Implementations.DTO
{
    internal class WarehouseEntryDTO : IWarehouseEntryDTO
    {
        public int Id { get; set; }

        public int productId { get; set; }

        public int quantity { get; set; }

        public WarehouseEntryDTO(int id, int productId, int quantity)
        {
            this.Id = id;
            this.productId = productId;
            this.quantity = quantity;
        }
    }
}
