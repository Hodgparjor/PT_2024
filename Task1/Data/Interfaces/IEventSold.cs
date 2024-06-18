
namespace Data.Interfaces
{
    public interface IEventSold : IEvent
    {
        int Id { get; set; }
        int WarehouseEntryId { get; set; }
        int Quantity { get; set; }
        int CustomerId { get; set; }
        int ProductId { get; set; }
    }
}
