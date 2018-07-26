using System.Windows;
using JobSearcher.Services;

namespace JobSearcher.Abstract
{
    internal abstract class DialogViewModel : BindableBase
    {
        #region Constructors

        protected DialogViewModel(string message, string heading)
        {
            Message = message;
            Heading = heading;
        }

        #endregion

        #region Properties

        public string Heading
        {
            get => Get<string>();
            private set => Set(value);
        }

        public string Message
        {
            get => Get<string>();
            private set => Set(value);
        }

        public DialogResult UserDialogResult
        {
            get => Get<DialogResult>();
            private set => Set(value);
        }

        #endregion

        #region Public Methods

        public void CloseDialogWithResult(Window dialog, DialogResult result)
        {
            UserDialogResult = result;
            if (dialog != null)
            {
                dialog.DialogResult = true;
            }
        }

        #endregion
    }
}