
namespace Data.Interfaces
{
    public interface IProduct
    {
        public string? Description { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Id { get; set; }
    }
}
