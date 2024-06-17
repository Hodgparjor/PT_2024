using Data.Interfaces;

namespace LogicTest.MockClasses
{
    internal class MockEvent : IEventSold
    {
        public MockEvent(int id, int stateId, int userId, int quantity)
        {
            this.Id = id;
            this.ProductId = stateId;
            this.WarehouseEntryId = stateId;
            this.UserId = userId;
            this.CustomerId = userId;
            this.Date = DateTime.Now;
            this.Quantity = quantity;
        }

        public int Id { get; set; }

        public int ProductId { get; set; }

        public int CustomerId { get; set; }
        public int WarehouseEntryId { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public int Quantity { get; set; }
    }
}
