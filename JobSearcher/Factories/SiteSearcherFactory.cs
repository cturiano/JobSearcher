using System;
using JobSearcher.Interfaces;
using JobSearcher.Services;
using JobSearcher.SiteSearchers;
using JobSearcher.Utils;

namespace JobSearcher.Factories
{
    internal static class SiteSearcherFactory
    {
        #region Public Methods

        public static ISiteSearcher MakeSiteSearcher(WebSource webSource, string searchString)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (webSource)
            {
                case WebSource.Craigslist:
                    return new CraigslistSiteSearcher(webSource, searchString, new DialogService(), UrlFetcherFactory.MakeUrlFetcher);
                case WebSource.Indeed:
                    return new IndeedSiteSearcher(webSource, searchString, new DialogService(), UrlFetcherFactory.MakeUrlFetcher);
                case WebSource.Monster:
                    return new MonsterSiteSearcher(webSource, searchString, new DialogService(), UrlFetcherFactory.MakeUrlFetcher);
                case WebSource.StackOverflow:
                    return new StackOverflowSiteSearcher(webSource, searchString, new DialogService(), UrlFetcherFactory.MakeUrlFetcher);
                case WebSource.Dice:
                    return new DiceSiteSearcher(webSource, searchString, new DialogService(), UrlFetcherFactory.MakeUrlFetcher);
                case WebSource.None:
                    return new UnknownSiteSearcher(webSource, searchString, new DialogService(), UrlFetcherFactory.MakeUrlFetcher);
                default:
                    throw new ArgumentOutOfRangeException(nameof(webSource), webSource, null);
            }
        }

        #endregion
    }
}