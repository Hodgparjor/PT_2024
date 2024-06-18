using Logic.API.DTO;


namespace PresentationTests.Mock
{
    internal class MockWarehouseEntryDTO : IWarehouseEntryDTO
    {
        public MockWarehouseEntryDTO(int id, int productId, int quantity)
        {
            this.Id = id;
            this.productId = productId;
            this.quantity = quantity;
        }

        public int Id { get; set; }

        public int productId { get; set; }

        public int quantity { get; set; }
    }
}
