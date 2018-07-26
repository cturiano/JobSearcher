using System.Windows;
using JobSearcher.Abstract;
using JobSearcher.Services;

namespace JobSearcher.Interfaces
{
    internal interface IDialogService
    {
        #region Public Methods

        DialogResult OpenDialog(DialogViewModel vm, Window owner);

        DialogResult ShowFileDialog(DialogViewModel vm, Window owner);

        #endregion
    }
}