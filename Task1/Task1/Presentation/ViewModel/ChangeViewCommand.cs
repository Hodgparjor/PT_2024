using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Presentation.ViewModel
{
    class ChangeViewCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private string _newViewModel;

        public ChangeViewCommand(string viewModel)
        {
            this._newViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            UserControl userControl = parameter as UserControl;

            Window parentWindow = Window.GetWindow(userControl);

            if (parentWindow != null)
            {
                if (parentWindow.DataContext is MainWindowVM mainWindowVM)
                {
                    switch (this._newViewModel)
                    {
                        case "ProductMasterView":
                            mainWindowVM.SelectedViewModel = new ProductMasterVM(); break;
                        case "UserMasterView":
                            mainWindowVM.SelectedViewModel = new UserMasterVM(); break;
                        case "WarehouseEntryMasterView":
                            mainWindowVM.SelectedViewModel = new WarehouseEntryMasterVM(); break;
                        case "EventMasterView":
                            mainWindowVM.SelectedViewModel = new EventMasterVM(); break;
                    }
                }
            }
        }
    }
}
