using System.Collections.Generic;

namespace JobSearcher.ViewModels
{
    internal class OpenFileViewModel : OkCancelViewModel
    {
        #region Constructors

        public OpenFileViewModel(string heading) : base(string.Empty, heading)
        {
            Paths = new List<string>();
        }

        #endregion

        #region Properties

        public List<string> Paths
        {
            get => Get<List<string>>();
            set => Set(value);
        }

        #endregion

        #region Protected Methods

        protected override void Cancel(object parameter)
        {
            Paths.Clear();
            Paths = null;
            base.Cancel(parameter);
        }

        protected override bool CanOk(object parameter)
        {
            return Paths != null && Paths.Count > 0;
        }

        #endregion
    }
}