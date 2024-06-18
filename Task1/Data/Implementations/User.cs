using Data.Interfaces;

namespace Data
{
    internal class User : IUser
    {
        private int id;
        private string name;

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
    }
}
