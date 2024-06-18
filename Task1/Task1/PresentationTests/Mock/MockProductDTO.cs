using Logic.API.DTO;


namespace PresentationTests.Mock
{
    internal class MockProductDTO : IProductDTO
    {
        public MockProductDTO(int id, string name, decimal price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Pegi { get; set; }
    }
}
