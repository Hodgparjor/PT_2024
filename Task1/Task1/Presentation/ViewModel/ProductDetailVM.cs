using Presentation.Model.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.ViewModel
{
    public class ProductDetailVM : ViewModelBase
    {
        public ICommand UpdateProduct { get; set; }

        private readonly IProductModelHandler _modelHandler;

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

        private decimal _price;

        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public ProductDetailVM(IProductModelHandler? model)
        {
            this.UpdateProduct = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelHandler = model ?? IProductModelHandler.CreateModelHandler();;
        }

        public ProductDetailVM()
        {
            this.UpdateProduct = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelHandler = IProductModelHandler.CreateModelHandler(); ;
        }

        public ProductDetailVM(int id, string name, decimal price, IProductModelHandler? model = null)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;

            this.UpdateProduct = new OnClickCommand(e => this.Update(), c => this.CanUpdate());

            this._modelHandler = model ?? IProductModelHandler.CreateModelHandler();
        }

        private void Update()
        {
            Task.Run(() =>
            {
                this._modelHandler.UpdateAsync(this.Id, this.Name, this.Price);
            });
        }

        private bool CanUpdate()
        {
            return !(
                string.IsNullOrWhiteSpace(this.Name) ||
                string.IsNullOrWhiteSpace(this.Price.ToString()) ||
                this.Price == 0
            );
        }
    }
}
