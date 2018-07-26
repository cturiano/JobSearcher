using System.Windows;
using System.Windows.Input;
using JobSearcher.Abstract;
using JobSearcher.Services;
using JobSearcher.Utils;

namespace JobSearcher.ViewModels
{
    internal class OkViewModel : DialogViewModel
    {
        #region Fields

        private RelayCommand _okCommand;

        #endregion

        #region Constructors

        public OkViewModel(string message, string heading) : base(message, heading)
        {
        }

        #endregion

        #region Properties

        public ICommand OkCommand => _okCommand ?? (_okCommand = new RelayCommand(Ok, CanOk));

        #endregion

        #region Protected Methods

        protected virtual bool CanOk(object obj)
        {
            return !(string.IsNullOrEmpty(Heading) || string.IsNullOrEmpty(Message));
        }

        protected virtual void Ok(object parameter)
        {
            CloseDialogWithResult(parameter as Window, DialogResult.OK);
        }

        #endregion
    }
}