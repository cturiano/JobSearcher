using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JobSearcher.Abstract;
using JobSearcher.UrlFetchers;
using JobSearcher.Utils;
using JobSearcherTest.Mocks;
using NUnit.Framework;

namespace JobSearcherTest.UrlFetchersTests
{
    internal class UrlFetcherTestsBase
    {
        #region Public Methods

        public virtual UrlFetcher ConstructorsAndPropertiesTest(WebSource webSource, string listFileString)
        {
            var fetcher = HelperMethods.UrlFetcherMockFactory(webSource, listFileString);
            Assert.IsNotNull(fetcher.URL);
            Assert.IsNotNull(fetcher.UrlQueryString);
            return fetcher;
        }

        public virtual async Task GetListingsAsyncTest(WebSource webSource, string listFileString, string baseUrl, string queryString, int count)
        {
            var fetcher = HelperMethods.UrlFetcherMockFactory(webSource, listFileString);
            var listings = await fetcher.GetListingsAsync(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../", listFileString)), CancellationToken.None);
            Assert.IsNotNull(listings);
            Assert.AreEqual(count, listings.Count());
        }

        public virtual async Task GetRemainingListingDataAsyncTest(WebSource webSource, string jobString)
        {
            var fetcher = HelperMethods.UrlFetcherMockFactory(webSource, jobString);
            var listing = new ListingBaseMock();
            listing = await fetcher.GetRemainingListingDataAsync(listing, CancellationToken.None) as ListingBaseMock;
            Assert.IsNotNull(listing);
            Assert.AreNotEqual("This is the description", listing.Description);
        }

        public virtual Task GetUrlsAsyncTest()
        {
            var fetcher = new IndeedUrlFetcher(() => new WebClientMock(""));
            Assert.ThrowsAsync(typeof(NotImplementedException), async () => await fetcher.GetUrlsAsync(CancellationToken.None));
            return Task.CompletedTask;
        }

        #endregion
    }
}