using Presentation.Model.API;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.ViewModel
{
    public class UserDetailVM : ViewModelBase
    {
        public ICommand UpdateUser { get; set; }

        private readonly IUserModelHandler _modelHandler;

        private int _id;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public UserDetailVM(IUserModelHandler? model = null)
        {
            this.UpdateUser = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelHandler = model ?? IUserModelHandler.CreateModelHandler(null);
        }

        public UserDetailVM(int id, string Name, IUserModelHandler? model = null)
        {
            this.Id = id;
            this.Name = Name;

            this.UpdateUser = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelHandler = model ?? IUserModelHandler.CreateModelHandler(null);
        }

        private void Update()
        {
            Task.Run(() =>
            {
                this._modelHandler.UpdateAsync(this.Id, this.Name);
            });
        }

        private bool CanUpdate()
        {
            return !(
                string.IsNullOrWhiteSpace(this.Name)
            );
        }
    }
}
