
namespace Data.Interfaces
{
    public interface ICustomer : IUser
    {
        public List<IWarehouseEntry> BoughtProducts { get; set; }
    }
}
