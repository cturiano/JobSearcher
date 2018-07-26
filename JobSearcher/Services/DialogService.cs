using System.Collections.Generic;
using System.Windows;
using JobSearcher.Abstract;
using JobSearcher.Interfaces;
using JobSearcher.ViewModels;
using JobSearcher.Views;
using Microsoft.Win32;

namespace JobSearcher.Services
{
    internal class DialogService : IDialogService
    {
        #region Public Methods

        public DialogResult OpenDialog(DialogViewModel vm, Window owner)
        {
            var win = new DialogWindow
                      {
                          Owner = owner ?? Application.Current.MainWindow,
                          DataContext = vm
                      };

            win.ShowDialog();
            var result = ((DialogViewModel)win.DataContext).UserDialogResult;
            return result;
        }

        public DialogResult ShowFileDialog(DialogViewModel vm, Window owner)
        {
            var fd = new OpenFileDialog
                     {
                         Multiselect = true,
                         CheckPathExists = true,
                         DefaultExt = "pdf",
                         Filter = "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*",
                         Title = vm.Heading
                     };

            var result = fd.ShowDialog(owner ?? Application.Current.MainWindow);
            if (vm is OpenFileViewModel ofvm)
            {
                ofvm.Paths = new List<string>(fd.FileNames);
            }

            return result.Value ? DialogResult.OK : DialogResult.Cancel;
        }

        #endregion
    }
}