using System.Windows;
using JobSearcher.Abstract;
using JobSearcher.Interfaces;
using JobSearcher.Services;

namespace JobSearcherTest.Mocks
{
    internal class DialogServiceMock : IDialogService
    {
        #region Public Methods

        public DialogResult OpenDialog(DialogViewModel vm, Window owner)
        {
            return DialogResult.OK;
        }

        public DialogResult ShowFileDialog(DialogViewModel vm, Window owner)
        {
            return DialogResult.OK;
        }

        #endregion
    }
}