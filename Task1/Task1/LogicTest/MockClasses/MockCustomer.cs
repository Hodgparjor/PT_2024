using Data;
using Data.Interfaces;

namespace LogicTest.MockClasses
{
    internal class MockCustomer : MockUser, ICustomer
    {
        private List<IWarehouseEntry> boughtProducts;

        public List<IWarehouseEntry> BoughtProducts
        {
            get { return boughtProducts; }
            set { boughtProducts = value; }
        }

        public MockCustomer(int id, string name) : base(id, name) { }

        public override bool Equals(Object? obj)
        {
            if (obj == null) throw new ArgumentNullException();

            if (obj is Customer customer)
            {
                if (customer.Id == this.Id && customer.Name.Equals(this.Name))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
