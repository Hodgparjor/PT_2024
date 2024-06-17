using Data.Interfaces;

namespace LogicTest.MockClasses
{
    internal class MockUser : ICustomer
    {
        private int id;
        private string name;
        private List<IWarehouseEntry> boughtProducts;

        public MockUser(int id, string name) 
        {
            this.id = id;
            this.name = name;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public List<IWarehouseEntry> BoughtProducts { get => boughtProducts; set => boughtProducts = value; }
    }
}
