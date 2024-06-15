using Presentation.Model.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Presentation.ViewModel
{
    public class EventMasterVM : ViewModelBase
    {
        public ICommand SwitchToUserMasterPage { get; set; }

        public ICommand SwitchToProductMasterPage { get; set; }

        public ICommand SwitchToWarehouseEntryMasterPage { get; set; }

        public ICommand CreateEvent { get; set; }

        public ICommand RemoveEvent { get; set; }

        private readonly IEventModelHandler _modelHandler;


        private ObservableCollection<EventDetailVM> _events;

        public ObservableCollection<EventDetailVM> Events
        {
            get => _events;
            set
            {
                _events = value;
                OnPropertyChanged(nameof(Events));
            }
        }

        private int _warehouseEntryId;

        public int WarehouseEntryId
        {
            get => _warehouseEntryId;
            set
            {
                _warehouseEntryId = value;
                OnPropertyChanged(nameof(WarehouseEntryId));
            }
        }

        private int _userId;

        public int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged(nameof(UserId));
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

        private bool _isEventSelected;

        public bool IsEventSelected
        {
            get => _isEventSelected;
            set
            {
                this.IsEventDetailVisible = value ? Visibility.Visible : Visibility.Hidden;

                _isEventSelected = value;
                OnPropertyChanged(nameof(IsEventSelected));
            }
        }

        private Visibility _isEventDetailVisible;

        public Visibility IsEventDetailVisible
        {
            get => _isEventDetailVisible;
            set
            {
                _isEventDetailVisible = value;
                OnPropertyChanged(nameof(IsEventDetailVisible));
            }
        }

        private EventDetailVM _selectedDetailViewModel;

        public EventDetailVM SelectedDetailViewModel
        {
            get => _selectedDetailViewModel;
            set
            {
                _selectedDetailViewModel = value;
                this.IsEventSelected = true;

                OnPropertyChanged(nameof(SelectedDetailViewModel));
            }
        }

        public EventMasterVM(IEventModelHandler? model = null)
        {
            this.SwitchToUserMasterPage = new ChangeViewCommand("UserMasterView");
            this.SwitchToWarehouseEntryMasterPage = new ChangeViewCommand("WarehouseEntryMasterView");
            this.SwitchToProductMasterPage = new ChangeViewCommand("ProductMasterView");

            this.CreateEvent = new OnClickCommand(e => this.StoreEvent(), c => this.CanStoreEvent());
            this.RemoveEvent = new OnClickCommand(e => this.DeleteEvent());

            this._events = new ObservableCollection<EventDetailVM>();
            OnPropertyChanged(nameof(Events));

            this._modelHandler = model ?? IEventModelHandler.CreateModelHandler(null);

            this.IsEventSelected = false;

            Task.Run(this.LoadEvents);
        }

        public EventMasterVM()
        {
            this.SwitchToUserMasterPage = new ChangeViewCommand("UserMasterView");
            this.SwitchToWarehouseEntryMasterPage = new ChangeViewCommand("WarehouseEntryMasterView");
            this.SwitchToProductMasterPage = new ChangeViewCommand("ProductMasterView");

            this.CreateEvent = new OnClickCommand(e => this.StoreEvent(), c => this.CanStoreEvent());
            this.RemoveEvent = new OnClickCommand(e => this.DeleteEvent());

            this._events = new ObservableCollection<EventDetailVM>();
            OnPropertyChanged(nameof(Events));

            this._modelHandler = IEventModelHandler.CreateModelHandler(null);

            this.IsEventSelected = false;

            Task.Run(this.LoadEvents);
        }

        private bool CanStoreEvent()
        {
            return !(
                string.IsNullOrWhiteSpace(this.WarehouseEntryId.ToString()) ||
                string.IsNullOrWhiteSpace(this.UserId.ToString()) || 
                this.Quantity < 1
            );
        }

        private void StoreEvent()
        {
            Task.Run(async () =>
            {
                int lastId = await this._modelHandler.GetCountAsync() + 1;

                await this._modelHandler.AddAsync(lastId, this.WarehouseEntryId, this.UserId, this.Quantity);

                this.LoadEvents();
            });
        }

        private void DeleteEvent()
        {
            Task.Run(async () =>
            {
                await this._modelHandler.DeleteAsync(this.SelectedDetailViewModel.Id);

                this.LoadEvents();

            });
        }

        private async void LoadEvents()
        {
            try
            {
                Dictionary<int, IEventModel> Events = (await this._modelHandler.GetAllAsync());

                Application.Current.Dispatcher.Invoke(() =>
                {
                    this._events.Clear();

                    foreach (IEventModel e in Events.Values)
                    {
                        this._events.Add(new EventDetailVM(e.Id, e.productId, e.userId, e.date, e.quantity));
                    }
                });

                OnPropertyChanged(nameof(Events));
            } catch (Exception ex) { }

        }
    }
}
