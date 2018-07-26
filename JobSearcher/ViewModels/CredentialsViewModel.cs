using JobSearcher.Models;

namespace JobSearcher.ViewModels
{
    internal class CredentialsViewModel : OkCancelViewModel
    {
        #region Constructors

        public CredentialsViewModel(string message, string heading) : base(message, heading)
        {
            Credentials = new Credentials();
        }

        #endregion

        #region Properties

        public Credentials Credentials
        {
            get => Get<Credentials>();
            set => Set(value);
        }

        #endregion

        #region Protected Methods

        protected override void Cancel(object parameter)
        {
            Credentials = null;
            base.Cancel(parameter);
        }

        protected override bool CanOk(object parameter)
        {
            return base.CanOk(new object()) && Credentials != null && !(string.IsNullOrEmpty(Credentials.Password) || string.IsNullOrEmpty(Credentials.UserName));
        }

        #endregion
    }
}