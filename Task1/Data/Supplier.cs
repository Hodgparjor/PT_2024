using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Supplier : User, ISupplier
    {
        private List<WarehouseEntry> suppliedProducts;
        public Supplier(int id, string name) 
        {
            Id = id;
            Name = name;
            suppliedProducts = new List<WarehouseEntry>();
        }

        public List<WarehouseEntry> SuppliedProducts { get => suppliedProducts; set => suppliedProducts = value; }

        public override bool Equals(object? obj)
        {
            return obj is Supplier supplier &&
                   Name == supplier.Name &&
                   Id == supplier.Id &&
                   EqualityComparer<List<WarehouseEntry>>.Default.Equals(SuppliedProducts, supplier.SuppliedProducts);
        }
    }
}
