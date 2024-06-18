using Logic.API.DTO;


namespace Logic.Implementations.DTO
{
    internal class ProductDTO : IProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }


        public ProductDTO(int id, string name, decimal price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }
    }
}
