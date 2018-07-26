using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JobSearcher.HtmlParsers;
using JobSearcher.Utils;
using JobSearcherTest.Mocks;
using JobSearcherTest.Properties;
using NUnit.Framework;

namespace JobSearcherTest.UrlFetchersTests
{
    [TestFixture]
    internal class CraigslistUrlFetcherTests : UrlFetcherTestsBase
    {
        [Test]
        public void ConstructorsAndPropertiesTest()
        {
            var fetcher = ConstructorsAndPropertiesTest(WebSource.Craigslist, Resources.CraigslistListFileString) as CraigslistUrlFetcherMock;
            Assert.IsNotNull(fetcher.MyParser);
            Assert.IsInstanceOf<CraigslistHtmlParser>(fetcher.MyParser);
        }

        [Test]
        public async Task GetListingsAsyncTest()
        {
            await GetListingsAsyncTest(WebSource.Craigslist, Resources.CraigslistListFileString, JobSearcher.Properties.Resources.CraigslistBaseUrl, JobSearcher.Properties.Resources.CraigslistQueryString, 120);
        }

        [Test]
        public async Task GetRemainingListingDataAsyncTest()
        {
            await GetRemainingListingDataAsyncTest(WebSource.Craigslist, Resources.CraigslistJobString);
        }

        [Test]
        public override async Task GetUrlsAsyncTest()
        {
            var fetcher = HelperMethods.UrlFetcherMockFactory(WebSource.Craigslist, Resources.CraigslistCitiesFileString) as CraigslistUrlFetcherMock;

            Assert.IsNotNull(fetcher.URL);
            Assert.IsNotNull(fetcher.UrlQueryString);
            Assert.IsNotNull(fetcher.MyParser);
            Assert.IsInstanceOf<CraigslistHtmlParser>(fetcher.MyParser);

            var cityLinks = await fetcher.GetUrlsAsync(CancellationToken.None);
            Assert.AreEqual(715, cityLinks.Count());
        }
    }
}