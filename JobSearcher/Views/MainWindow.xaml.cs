using System.ComponentModel;
using System.Windows.Controls;
using JobSearcher.Services;
using JobSearcher.ViewModels;

namespace JobSearcher.Views
{
    /// <inheritdoc cref="System.Windows.Window" />
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Constructors

        public MainWindow()
        {
            DataContext = new MainViewModel(new DialogService());
            InitializeComponent();
        }

        #endregion

        #region Public Methods

        public static string GetPropertyDisplayName(object descriptor)
        {
            var pd = (PropertyDescriptor)descriptor;
            var displayName = (DisplayNameAttribute)pd?.Attributes[typeof(DisplayNameAttribute)];

            if (displayName != null && !displayName.Equals(DisplayNameAttribute.Default))
            {
                return displayName.DisplayName;
            }

            return null;
        }

        #endregion

        #region Private Methods

        private void JobGrid_OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var displayName = GetPropertyDisplayName(e.PropertyDescriptor);
            if (!string.IsNullOrEmpty(displayName))
            {
                e.Column.Header = displayName;
                e.Column.IsReadOnly = true;
            }
            else
            {
                e.Cancel = true;
            }
        }

        #endregion
    }
}