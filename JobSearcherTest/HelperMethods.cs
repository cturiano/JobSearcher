using System;
using System.IO;
using HtmlAgilityPack;
using JobSearcher.Abstract;
using JobSearcher.Interfaces;
using JobSearcher.Models;
using JobSearcher.SiteSearchers;
using JobSearcher.Utils;
using JobSearcherTest.Mocks;

namespace JobSearcherTest
{
    internal static class HelperMethods
    {
        #region Public Methods

        public static HtmlNode GetNodeFromHtml(string html, string xPath)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            return doc.DocumentNode.SelectSingleNode(xPath);
        }

        public static ListingBase ListingFactory(WebSource webSource, HtmlNode node)
        {
            ListingBase listing;

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (webSource)
            {
                case WebSource.Craigslist:
                    listing = new CraigslistListing();
                    break;
                case WebSource.Indeed:
                    listing = new IndeedListing();
                    break;
                case WebSource.Monster:
                    listing = new MonsterListing();
                    break;
                case WebSource.StackOverflow:
                    listing = new StackOverflowListing();
                    break;
                case WebSource.Dice:
                    listing = new DiceListing();
                    break;
                case WebSource.None:
                    listing = new UnknownListing();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(webSource), webSource, null);
            }

            listing.ParseInitialNode(node);
            return listing;
        }

        public static string LoadFromFile(string filePath)
        {
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../", filePath);
            return File.Exists(filePath) ? File.ReadAllText(filePath) : string.Empty;
        }

        public static ISiteSearcher MakeSiteSearcher(WebSource webSource, string searchString)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (webSource)
            {
                case WebSource.Craigslist:
                    return new CraigslistSiteSearcher(webSource, searchString, new DialogServiceMock(), UrlFetcherMockFactory);
                case WebSource.Indeed:
                    return new IndeedSiteSearcher(webSource, searchString, new DialogServiceMock(), UrlFetcherMockFactory);
                case WebSource.Monster:
                    return new MonsterSiteSearcher(webSource, searchString, new DialogServiceMock(), UrlFetcherMockFactory);
                case WebSource.StackOverflow:
                    return new StackOverflowSiteSearcher(webSource, searchString, new DialogServiceMock(), UrlFetcherMockFactory);
                case WebSource.Dice:
                    return new DiceSiteSearcher(webSource, searchString, new DialogServiceMock(), UrlFetcherMockFactory);
                case WebSource.None:
                    return new UnknownSiteSearcher(webSource, searchString, new DialogServiceMock(), UrlFetcherMockFactory);
                default:
                    throw new ArgumentOutOfRangeException(nameof(webSource), webSource, null);
            }
        }

        public static UrlFetcher UrlFetcherMockFactory(WebSource webSource, string filePath)
        {
            WebClientMock Func()
            {
                return new WebClientMock(filePath);
            }

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (webSource)
            {
                case WebSource.Craigslist:
                    return new CraigslistUrlFetcherMock((Func<WebClientMock>)Func);
                case WebSource.Indeed:
                    return new IndeedUrlFetcherMock((Func<WebClientMock>)Func);
                case WebSource.Monster:
                    return new MonsterUrlFetcherMock((Func<WebClientMock>)Func);
                case WebSource.StackOverflow:
                    return new StackOverflowUrlFetcherMock((Func<WebClientMock>)Func);
                case WebSource.Dice:
                    return new DiceUrlFetcherMock((Func<WebClientMock>)Func);
                case WebSource.None:
                    return new UnknownUrlFetcherMock((Func<WebClientMock>)Func);
                default:
                    throw new ArgumentOutOfRangeException(nameof(webSource), webSource, null);
            }
        }

        #endregion
    }
}