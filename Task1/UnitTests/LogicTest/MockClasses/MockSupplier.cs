using Data;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UnitTests.LogicTest.MockClasses
{
    internal class MockSupplier : MockUser, ISupplier 
    {
        private List<IWarehouseEntry> suppliedProducts;
        public MockSupplier(int id, string name)
        {
            Id = id;
            Name = name;
            suppliedProducts = new List<IWarehouseEntry>();
        }

        public List<IWarehouseEntry> SuppliedProducts { get => suppliedProducts; set => suppliedProducts = value; }

        public override bool Equals(object? obj)
        {
            return obj is MockSupplier supplier &&
                   Name == supplier.Name &&
                   Id == supplier.Id &&
                   EqualityComparer<List<IWarehouseEntry>>.Default.Equals(SuppliedProducts, supplier.SuppliedProducts);
        }
    }
}
