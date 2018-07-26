using System;
using System.IO;
using System.Threading.Tasks;
using JobSearcher.Abstract;
using JobSearcher.Interfaces;

namespace JobSearcherTest.Mocks
{
    internal class WebClientMock : BindableBase, IWebClient
    {
        #region Constructors

        public WebClientMock(string filePath)
        {
            FilePath = filePath;
        }

        #endregion

        #region Properties

        public string FilePath
        {
            get => Get<string>();
            private set => Set(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../", value));
        }

        #endregion

        #region Public Methods

        public void Dispose()
        {}

        public async Task<string> DownloadStringTaskAsync(Uri uri)
        {
            return await LoadFromFile();
        }

        #endregion

        #region Private Methods

        private async Task<string> LoadFromFile()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                throw new ArgumentNullException(nameof(FilePath));
            }

            return await Task.Run(() => File.Exists(FilePath) ? File.ReadAllText(FilePath) : string.Empty);
        }

        #endregion
    }
}