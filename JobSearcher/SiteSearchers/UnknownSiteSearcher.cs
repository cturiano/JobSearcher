using System;
using JobSearcher.Abstract;
using JobSearcher.Interfaces;
using JobSearcher.Utils;

namespace JobSearcher.SiteSearchers
{
    internal class UnknownSiteSearcher : SiteSearcher
    {
        #region Constructors

        public UnknownSiteSearcher(WebSource webSource, string searchString, IDialogService dialogService, Func<WebSource, string, IUrlFetcher> urlFetcherFactory) : base(webSource, searchString, dialogService, urlFetcherFactory)
        {
        }

        #endregion
    }
}