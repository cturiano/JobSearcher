using System.Windows;
using System.Windows.Input;
using JobSearcher.Abstract;
using JobSearcher.Services;
using JobSearcher.Utils;

namespace JobSearcher.ViewModels
{
    internal class YesNoViewModel : DialogViewModel
    {
        #region Fields

        private RelayCommand _noCommand;
        private RelayCommand _yesCommand;

        #endregion

        #region Constructors

        public YesNoViewModel(string message, string heading) : base(message, heading)
        {
        }

        #endregion

        #region Properties

        public ICommand NoCommand => _noCommand ?? (_noCommand = new RelayCommand(No));

        public ICommand YesCommand => _yesCommand ?? (_yesCommand = new RelayCommand(Yes));

        #endregion

        #region Private Methods

        private void No(object parameter)
        {
            CloseDialogWithResult(parameter as Window, DialogResult.No);
        }

        private void Yes(object parameter)
        {
            CloseDialogWithResult(parameter as Window, DialogResult.Yes);
        }

        #endregion
    }
}