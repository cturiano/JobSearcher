using System;
using JobSearcher.Abstract;
using JobSearcher.Interfaces;
using JobSearcher.Properties;
using JobSearcher.Utils;

namespace JobSearcher.SiteSearchers
{
    internal class DiceSiteSearcher : SiteSearcher
    {
        #region Constructors

        public DiceSiteSearcher(WebSource webSource, string searchString, IDialogService dialogService, Func<WebSource, string, IUrlFetcher> urlFetcherFactory) : base(webSource, searchString, dialogService, urlFetcherFactory)
        {
            SiteUrl = Resources.DiceBaseUrl;
            if (string.IsNullOrEmpty(SearchString))
            {
                SearchString = Resources.DiceQueryString;
            }
        }

        #endregion
    }
}