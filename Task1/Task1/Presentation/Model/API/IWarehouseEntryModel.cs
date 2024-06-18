namespace Presentation.Model.API
{
    public interface IWarehouseEntryModel
    {
        public int Id { get; set; }
        public int productId { get; set; }
        public int quantity { get; set; }
    }
}
