namespace Presentation.ViewModel
{
    internal class MainWindowVM : ViewModelBase
    {
        private ViewModelBase _selectedViewModel { get; set; }


        public MainWindowVM()
        {
            this._selectedViewModel = new ProductMasterVM();
        }

        public new ViewModelBase SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;

                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }
    }
}
