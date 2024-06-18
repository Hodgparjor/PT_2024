
namespace Data.Interfaces
{
    public interface ISupplier : IUser
    {
        public List<IWarehouseEntry> SuppliedProducts { get; set; }
    }
}
