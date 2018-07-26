using System;
using System.Threading.Tasks;

namespace JobSearcher.Interfaces
{
    internal interface IWebClient : IDisposable
    {
        Task<string> DownloadStringTaskAsync(Uri uri);
    }
}