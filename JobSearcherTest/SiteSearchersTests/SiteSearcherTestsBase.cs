using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JobSearcher.Utils;
using JobSearcherTest.Mocks;
using NUnit.Framework;

namespace JobSearcherTest.SiteSearchersTests
{
    internal class SiteSearcherTestsBase
    {
        #region Public Methods

        public void ConstructorsAndPropertiesTest(WebSource webSource, string queryString)
        {
            var searcher = new SiteSearcherMock(webSource, queryString, new DialogServiceMock(), HelperMethods.UrlFetcherMockFactory);

            Assert.IsNotNull(searcher.SearchString);
            Assert.AreEqual(queryString, searcher.SearchString);
            Assert.IsNotNull(searcher.Listings);
            Assert.AreEqual(0, searcher.Listings.Count);
            Assert.IsNotNull(searcher.MyFetcher);
            Assert.IsNotNull(searcher.MyDialogService);
        }

        public async Task SearchAsyncTest(WebSource webSource, string queryString, int count)
        {
            var searcher = HelperMethods.MakeSiteSearcher(webSource, queryString);
            var listings = await searcher.SearchAsync(CancellationToken.None);
            Assert.AreEqual(count, listings.Count());
        }

        #endregion
    }
}