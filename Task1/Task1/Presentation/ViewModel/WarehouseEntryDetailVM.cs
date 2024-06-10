using Presentation.Model.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.ViewModel
{
    internal class WarehouseEntryDetailVM : ViewModelBase
    {
        public ICommand UpdateState { get; set; }

        private readonly IWarehouseEntryModelHandler _modelHandler;

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

        public WarehouseEntryDetailVM(IWarehouseEntryModelHandler? model = null)
        {
            this.UpdateState = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelHandler = IWarehouseEntryModelHandler.CreateModelHandler();
        }

        public WarehouseEntryDetailVM(int id, int productId, int quantity, IWarehouseEntryModelHandler? model = null)
        {
            this.Id = id;
            this.ProductId = productId;
            this.Quantity = quantity;

            this.UpdateState = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelHandler = IWarehouseEntryModelHandler.CreateModelHandler();
        }

        private void Update()
        {
            Task.Run(() =>
            {
                this._modelHandler.UpdateAsync(this.Id, this.ProductId, this.Quantity);
            });
        }

        private bool CanUpdate()
        {
            return !(
                string.IsNullOrWhiteSpace(this.Quantity.ToString()) ||
                this.Quantity < 0
            );
        }
    }
}
