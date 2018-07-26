using System;
using JobSearcher.Abstract;
using JobSearcher.Interfaces;
using JobSearcher.Properties;
using JobSearcher.Utils;

namespace JobSearcher.SiteSearchers
{
    internal class IndeedSiteSearcher : SiteSearcher
    {
        #region Constructors

        public IndeedSiteSearcher(WebSource webSource, string searchString, IDialogService dialogService, Func<WebSource, string, IUrlFetcher> urlFetcherFactory) : base(webSource, searchString, dialogService, urlFetcherFactory)
        {
            SiteUrl = Resources.IndeedBaseUrl;
            if (string.IsNullOrEmpty(SearchString))
            {
                SearchString = Resources.IndeedQueryString;
            }
        }

        #endregion
    }
}