using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _selectedViewModel { get; set; }


        public MainWindowViewModel()
        {
            this._selectedViewModel = new ProductMasterVM();
        }

        public ViewModelBase SelectedViewModel
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
