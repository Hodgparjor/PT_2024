using Data.Interfaces;

namespace Data
{
    internal class Supplier : User, ISupplier
    {
        private List<IWarehouseEntry> suppliedProducts;
        public Supplier(int id, string name) 
        {
            Id = id;
            Name = name;
            suppliedProducts = new List<IWarehouseEntry>();
        }

        public List<IWarehouseEntry> SuppliedProducts { get => suppliedProducts; set => suppliedProducts = value; }

        public override bool Equals(object? obj)
        {
            return obj is Supplier supplier &&
                   Name == supplier.Name &&
                   Id == supplier.Id &&
                   EqualityComparer<List<IWarehouseEntry>>.Default.Equals(SuppliedProducts, supplier.SuppliedProducts);
        }
    }
}
