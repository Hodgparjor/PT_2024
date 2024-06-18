
namespace Data.Interfaces
{
    public interface IWarehouseEntry
    {
        public int Quantity { get; set; }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public IProduct Product { get; set; }
    }
}
