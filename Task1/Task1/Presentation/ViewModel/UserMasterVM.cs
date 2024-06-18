using Presentation.Model.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Presentation.ViewModel
{
    public class UserMasterVM : ViewModelBase
    {
        public ICommand SwitchToProductMasterPage { get; set; }

        public ICommand SwitchToWarehouseEntryMasterPage { get; set; }

        public ICommand SwitchToEventMasterPage { get; set; }

        public ICommand CreateUser { get; set; }

        public ICommand RemoveUser { get; set; }

        private readonly IUserModelHandler _modelHandler;

        private ObservableCollection<UserDetailVM> _users;

        public ObservableCollection<UserDetailVM> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        private string _Name;

        public string Name
        {
            get => _Name;
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private bool _isUserSelected;

        public bool IsUserSelected
        {
            get => _isUserSelected;
            set
            {
                this.IsUserDetailVisible = value ? Visibility.Visible : Visibility.Hidden;

                _isUserSelected = value;
                OnPropertyChanged(nameof(IsUserSelected));
            }
        }

        private Visibility _isUserDetailVisible;

        public Visibility IsUserDetailVisible
        {
            get => _isUserDetailVisible;
            set
            {
                _isUserDetailVisible = value;
                OnPropertyChanged(nameof(IsUserDetailVisible));
            }
        }

        private UserDetailVM _selectedDetailViewModel;

        public UserDetailVM SelectedDetailViewModel
        {
            get => _selectedDetailViewModel;
            set
            {
                _selectedDetailViewModel = value;
                this.IsUserSelected = true;

                OnPropertyChanged(nameof(SelectedDetailViewModel));
            }
        }

        public UserMasterVM(IUserModelHandler? model = null)
        {
            this.SwitchToProductMasterPage = new ChangeViewCommand("ProductMasterView");
            this.SwitchToWarehouseEntryMasterPage = new ChangeViewCommand("WarehouseEntryMasterView");
            this.SwitchToEventMasterPage = new ChangeViewCommand("EventMasterView");

            this.CreateUser = new OnClickCommand(e => this.StoreUser(), c => this.CanStoreUser());
            this.RemoveUser = new OnClickCommand(e => this.DeleteUser());

            this.Users = new ObservableCollection<UserDetailVM>();

            this._modelHandler = model ?? IUserModelHandler.CreateModelHandler();

            this.IsUserSelected = false;

            Task.Run(this.LoadUsers);
        }

        private bool CanStoreUser()
        {
            return !(
                string.IsNullOrWhiteSpace(this.Name)
            );
        }

        private void StoreUser()
        {
            Task.Run(async () =>
            {
                int lastId = await this._modelHandler.GetCountAsync() + 1;

                await this._modelHandler.AddAsync(lastId, this.Name);

                this.LoadUsers();
            });
        }

        private void DeleteUser()
        {
            Task.Run(async () =>
            {
                try
                {
                    await this._modelHandler.DeleteAsync(this.SelectedDetailViewModel.Id);

                    this.LoadUsers();
                }
                catch (Exception e)
                {

                }
            });
        }

        private async void LoadUsers()
        {
            Dictionary<int, IUserModel> Users = await this._modelHandler.GetAllAsync();

            Application.Current.Dispatcher.Invoke(() =>
            {
                this._users.Clear();

                foreach (IUserModel u in Users.Values)
                {
                    this._users.Add(new UserDetailVM(u.Id, u.Name));
                }
            });

            OnPropertyChanged(nameof(Users));
        }
    }
}
