using Logic.API.DTO;


namespace PresentationTests.Mock
{
    internal class MockUserDTO : IUserDTO
    {
        public MockUserDTO(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
