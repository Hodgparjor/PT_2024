using Presentation.Model.API;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.ViewModel
{
    public class EventDetailVM : ViewModelBase
    {
        public ICommand UpdateEvent { get; set; }

        private readonly IEventModelHandler _modelHandler;

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

        private DateTime _date;

        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
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

        public EventDetailVM(IEventModelHandler? model = null)
        {
            this.UpdateEvent = new OnClickCommand(e => this.Update(), c => this.CanUpdate());
            this._modelHandler = IEventModelHandler.CreateModelHandler(null);
        }

        public EventDetailVM(int id, int warehouseEntryId, int userId, DateTime date, int quantity, IEventModelHandler? model = null)
        {
            this.UpdateEvent = new OnClickCommand(e => this.Update(), c => this.CanUpdate());
            this._modelHandler = IEventModelHandler.CreateModelHandler(null);

            this.Id = id;
            this.WarehouseEntryId = warehouseEntryId;
            this.UserId = userId;
            this.Date = date;
            this.Quantity = quantity;
        }

        private void Update()
        {
            Task.Run(async () =>
            {
                await this._modelHandler.UpdateAsync(this.Id, this.WarehouseEntryId, this.UserId, this.Date, this.Quantity);
            });
        }

        private bool CanUpdate()
        {
            return !(
                string.IsNullOrWhiteSpace(this.Date.ToString())
            );
        }
    }
}
