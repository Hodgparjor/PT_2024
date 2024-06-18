using Logic.API.DTO;

namespace Logic.Implementations.DTO
{
    internal class UserDTO : IUserDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public UserDTO(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
