using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Presentation.Model.API;

namespace Presentation.ViewModel
{
    public class WarehouseEntryMasterVM : ViewModelBase
    {
        public ICommand SwitchToUserMasterPage { get; set; }

        public ICommand SwitchToProductMasterPage { get; set; }

        public ICommand SwitchToEventMasterPage { get; set; }

        public ICommand CreateWarehouseEntry { get; set; }

        public ICommand RemoveWarehouseEntry { get; set; }

        private readonly IWarehouseEntryModelHandler _modelHandler;

        private ObservableCollection<WarehouseEntryDetailVM> _warehouseEntries;

        public ObservableCollection<WarehouseEntryDetailVM> WarehouseEntries
        {
            get => _warehouseEntries;
            set
            {
                _warehouseEntries = value;
                OnPropertyChanged(nameof(WarehouseEntries));
            }
        }

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

        private int _productId;

        public int ProductId
        {
            get => _productId;
            set
            {
                _productId = value;
                OnPropertyChanged(nameof(ProductId));
            }
        }

        private int _quantity;

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        private bool _isWarehouseEntrySelected;

        public bool IsWarehouseEntrySelected
        {
            get => _isWarehouseEntrySelected;
            set
            {
                this.IsWarehouseEntryDetailVisible = value ? Visibility.Visible : Visibility.Hidden;

                _isWarehouseEntrySelected = value;
                OnPropertyChanged(nameof(IsWarehouseEntrySelected));
            }
        }

        private Visibility _isWarehouseEntryDetailVisible;

        public Visibility IsWarehouseEntryDetailVisible
        {
            get => _isWarehouseEntryDetailVisible;
            set
            {
                _isWarehouseEntryDetailVisible = value;
                OnPropertyChanged(nameof(IsWarehouseEntryDetailVisible));
            }
        }

        private WarehouseEntryDetailVM _selectedDetailViewModel;

        public WarehouseEntryDetailVM SelectedDetailViewModel
        {
            get => _selectedDetailViewModel;
            set
            {
                _selectedDetailViewModel = value;
                this.IsWarehouseEntrySelected = true;

                OnPropertyChanged(nameof(SelectedDetailViewModel));
            }
        }

        public WarehouseEntryMasterVM()
        {
            this.SwitchToUserMasterPage = new ChangeViewCommand("UserMasterView");
            this.SwitchToProductMasterPage = new ChangeViewCommand("ProductMasterView");
            this.SwitchToEventMasterPage = new ChangeViewCommand("EventMasterView");

            this.CreateWarehouseEntry = new OnClickCommand(e => this.StoreWarehouseEntry(), c => this.CanStoreWarehouseEntry());
            this.RemoveWarehouseEntry = new OnClickCommand(e => this.DeleteWarehouseEntry());

            this.WarehouseEntries = new ObservableCollection<WarehouseEntryDetailVM>();

            this._modelHandler = IWarehouseEntryModelHandler.CreateModelHandler(null);

            this.IsWarehouseEntrySelected = false;

            Task.Run(this.LoadWarehouseEntries);
        }

        public WarehouseEntryMasterVM(IWarehouseEntryModelHandler? model = null)
        {
            this.SwitchToUserMasterPage = new ChangeViewCommand("UserMasterView");
            this.SwitchToProductMasterPage = new ChangeViewCommand("ProductMasterView");
            this.SwitchToEventMasterPage = new ChangeViewCommand("EventMasterView");

            this.CreateWarehouseEntry = new OnClickCommand(e => this.StoreWarehouseEntry(), c => this.CanStoreWarehouseEntry());
            this.RemoveWarehouseEntry = new OnClickCommand(e => this.DeleteWarehouseEntry());

            this.WarehouseEntries = new ObservableCollection<WarehouseEntryDetailVM>();

            this._modelHandler = model ?? IWarehouseEntryModelHandler.CreateModelHandler(null);

            this.IsWarehouseEntrySelected = false;

            Task.Run(this.LoadWarehouseEntries);
        }

        private bool CanStoreWarehouseEntry()
        {
            return !(
                string.IsNullOrWhiteSpace(this.ProductId.ToString()) ||
                string.IsNullOrWhiteSpace(this.Quantity.ToString()) ||
                this.Quantity < 0
            );
        }

        private void StoreWarehouseEntry()
        {
            Task.Run(async () =>
            {
                try
                {
                    int lastId = await this._modelHandler.GetCountAsync() + 1;

                    await this._modelHandler.AddAsync(lastId, this.ProductId, this.Quantity);

                    this.LoadWarehouseEntries();
                }
                catch (Exception e) { }
            });
        }

        private void DeleteWarehouseEntry()
        {
            Task.Run(async () =>
            {
                try
                {
                    await this._modelHandler.DeleteAsync(this.SelectedDetailViewModel.Id);

                    this.LoadWarehouseEntries();
                }
                catch (Exception e) { }
            });
        }

        private async void LoadWarehouseEntries()
        {
            Dictionary<int, IWarehouseEntryModel> WarehouseEntries = await this._modelHandler.GetAllAsync();

            Application.Current.Dispatcher.Invoke(() =>
            {
                this._warehouseEntries.Clear();

                foreach (IWarehouseEntryModel s in WarehouseEntries.Values)
                {
                    this._warehouseEntries.Add(new WarehouseEntryDetailVM(s.Id, s.productId, s.quantity));
                }
            });

            OnPropertyChanged(nameof(WarehouseEntries));
        }
    }
}
