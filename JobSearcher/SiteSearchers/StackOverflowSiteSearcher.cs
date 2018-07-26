using System;
using JobSearcher.Abstract;
using JobSearcher.Interfaces;
using JobSearcher.Properties;
using JobSearcher.Utils;

namespace JobSearcher.SiteSearchers
{
    internal class StackOverflowSiteSearcher : SiteSearcher
    {
        #region Constructors

        public StackOverflowSiteSearcher(WebSource webSource, string searchString, IDialogService dialogService, Func<WebSource, string, IUrlFetcher> urlFetcherFactory) : base(webSource, searchString, dialogService, urlFetcherFactory)
        {
            SiteUrl = Resources.StackOverFlowBaseUrl;
            if (string.IsNullOrEmpty(SearchString))
            {
                SearchString = Resources.StackOverFlowQueryString;
            }
        }

        #endregion
    }
}