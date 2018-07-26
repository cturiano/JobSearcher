using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JobSearcher.Abstract;
using JobSearcher.Interfaces;
using JobSearcher.Services;
using JobSearcher.UrlFetchers;
using JobSearcher.Utils;
using NUnit.Framework;

namespace JobSearcherTest.AbstractTests
{
    [TestFixture]
    internal class SiteSearcherTests
    {
        private class TestSiteSearcher : SiteSearcher
        {
            #region Constructors

            public TestSiteSearcher(WebSource webSource, string searchString, IDialogService dialogService, Func<WebSource, string, IUrlFetcher> urlFetcherFactory) : base(webSource, searchString, dialogService, urlFetcherFactory)
            {
            }

            #endregion

            #region Public Methods

            public IUrlFetcher GetFetcher()
            {
                return Fetcher;
            }

            public override async Task<IEnumerable<ListingBase>> SearchAsync(CancellationToken ct)
            {
                return await Task.Run(() => Listings, ct);
            }

            #endregion
        }

        [Test]
        public void ConstructorAndPropertiesTests()
        {
            var ss = new TestSiteSearcher(WebSource.Craigslist, "searchString", new DialogService(), HelperMethods.UrlFetcherMockFactory);
            Assert.AreEqual("searchString", ss.SearchString);
            Assert.AreEqual(new List<ListingBase>(), ss.Listings);
            Assert.IsInstanceOf<CraigslistUrlFetcher>(ss.GetFetcher());
        }
    }
}