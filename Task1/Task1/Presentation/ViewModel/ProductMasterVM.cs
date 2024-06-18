using Presentation.Model.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Presentation.ViewModel
{
    public class ProductMasterVM : ViewModelBase
    {
        public ICommand SwitchToUserMasterPage { get; set; }

        public ICommand SwitchToWarehouseEntryMasterPage { get; set; }

        public ICommand SwitchToEventMasterPage { get; set; }

        public ICommand CreateProduct { get; set; }

        public ICommand RemoveProduct { get; set; }

        private readonly IProductModelHandler _modelHandler;

        private ObservableCollection<ProductDetailVM> _products;

        public ObservableCollection<ProductDetailVM> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
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


        private bool _isProductSelected;

        public bool IsProductSelected
        {
            get => _isProductSelected;
            set
            {
                this.IsProductDetailVisible = value ? Visibility.Visible : Visibility.Hidden;

                _isProductSelected = value;
                OnPropertyChanged(nameof(IsProductSelected));
            }
        }

        private Visibility _isProductDetailVisible;

        public Visibility IsProductDetailVisible
        {
            get => _isProductDetailVisible;
            set
            {
                _isProductDetailVisible = value;
                OnPropertyChanged(nameof(IsProductDetailVisible));
            }
        }

        private ProductDetailVM _selectedDetailViewModel;

        public ProductDetailVM SelectedDetailViewModel
        {
            get => _selectedDetailViewModel;
            set
            {
                _selectedDetailViewModel = value;
                this.IsProductSelected = true;

                OnPropertyChanged(nameof(SelectedDetailViewModel));
            }
        }

        public ProductMasterVM(IProductModelHandler? model = null)
        {
            this.SwitchToUserMasterPage = new ChangeViewCommand("UserMasterView");
            this.SwitchToWarehouseEntryMasterPage = new ChangeViewCommand("WarehouseEntryMasterView");
            this.SwitchToEventMasterPage = new ChangeViewCommand("EventMasterView");

            this.CreateProduct = new OnClickCommand(e => this.StoreProduct(), c => this.CanStoreProduct());
            this.RemoveProduct = new OnClickCommand(e => this.DeleteProduct());

            this.Products = new ObservableCollection<ProductDetailVM>();

            this._modelHandler = model ?? IProductModelHandler.CreateModelHandler(null);

            this.IsProductSelected = false;

            Task.Run(this.LoadProducts);
        }

        public ProductMasterVM()
        {
            this.SwitchToUserMasterPage = new ChangeViewCommand("UserMasterView");
            this.SwitchToWarehouseEntryMasterPage = new ChangeViewCommand("WarehouseEntryMasterView");
            this.SwitchToEventMasterPage = new ChangeViewCommand("EventMasterView");

            this.CreateProduct = new OnClickCommand(e => this.StoreProduct(), c => this.CanStoreProduct());
            this.RemoveProduct = new OnClickCommand(e => this.DeleteProduct());

            this.Products = new ObservableCollection<ProductDetailVM>();

            this._modelHandler = IProductModelHandler.CreateModelHandler(null);

            this.IsProductSelected = false;

            Task.Run(this.LoadProducts);
        }

        private bool CanStoreProduct()
        {
            return !(
                string.IsNullOrWhiteSpace(this.Name) ||
                string.IsNullOrWhiteSpace(this.Price.ToString()) ||
                this.Price <= 0
            );
        }

        private void StoreProduct()
        {
            Task.Run(async () =>
            {
                int lastId = await this._modelHandler.GetCountAsync() + 1;

                await this._modelHandler.AddAsync(lastId, this.Name, this.Price);

                this.LoadProducts();
            });
        }

        private void DeleteProduct()
        {
            Task.Run(async () =>
            {
                try
                {
                    await this._modelHandler.DeleteAsync(this.SelectedDetailViewModel.Id);

                    this.LoadProducts();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            });
        }

        private async void LoadProducts()
        {
            try
            {
                Dictionary<int, IProductModel> Products = await this._modelHandler.GetAllAsync();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    this._products.Clear();

                    foreach (IProductModel p in Products.Values)
                    {
                        this._products.Add(new ProductDetailVM(p.Id, p.Name, p.Price));
                    }
                });

                OnPropertyChanged(nameof(Products));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading products: {ex.Message}");
            }
        }
    }
}
