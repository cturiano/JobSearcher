using System;
using JobSearcher.Abstract;
using JobSearcher.Interfaces;
using JobSearcher.Properties;
using JobSearcher.Utils;

namespace JobSearcher.SiteSearchers
{
    internal class MonsterSiteSearcher : SiteSearcher
    {
        #region Constructors

        public MonsterSiteSearcher(WebSource webSource, string searchString, IDialogService dialogService, Func<WebSource, string, IUrlFetcher> urlFetcherFactory) : base(webSource, searchString, dialogService, urlFetcherFactory)
        {
            SiteUrl = Resources.MonsterBaseUrl;
            if (string.IsNullOrEmpty(SearchString))
            {
                SearchString = Resources.MonsterQueryString;
            }
        }

        #endregion
    }
}