using Presentation.Model.API;

namespace Presentation.Model
{
    internal class UserModel : IUserModel
    {
        public UserModel(int id, string name) {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
