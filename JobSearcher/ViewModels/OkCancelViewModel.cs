using System.Windows;
using System.Windows.Input;
using JobSearcher.Services;
using JobSearcher.Utils;

namespace JobSearcher.ViewModels
{
    internal class OkCancelViewModel : OkViewModel
    {
        #region Fields

        private RelayCommand _cancelCommand;

        #endregion

        #region Constructors

        public OkCancelViewModel(string message, string heading) : base(message, heading)
        {
        }

        #endregion

        #region Properties

        public ICommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(Cancel));

        #endregion

        #region Protected Methods

        protected virtual void Cancel(object parameter)
        {
            CloseDialogWithResult(parameter as Window, DialogResult.Cancel);
        }

        #endregion
    }
}