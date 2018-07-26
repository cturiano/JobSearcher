using System.Threading.Tasks;
using JobSearcher.HtmlParsers;
using JobSearcher.Properties;
using JobSearcher.Utils;
using JobSearcherTest.Mocks;
using NUnit.Framework;
using TestResources = JobSearcherTest.Properties.Resources;

namespace JobSearcherTest.UrlFetchersTests
{
    [TestFixture]
    internal class IndeedUrlFetcherTests : UrlFetcherTestsBase
    {
        [Test]
        public void ConstructorsAndPropertiesTest()
        {
            var fetcher = ConstructorsAndPropertiesTest(WebSource.Indeed, TestResources.IndeedListFileString) as IndeedUrlFetcherMock;
            Assert.IsNotNull(fetcher.MyParser);
            Assert.IsInstanceOf<IndeedHtmlParser>(fetcher.MyParser);
        }

        [Test]
        public async Task GetListingsAsyncTest()
        {
            await GetListingsAsyncTest(WebSource.Indeed, TestResources.IndeedListFileString, Resources.IndeedBaseUrl, Resources.IndeedQueryString, 11);
        }

        [Test]
        public async Task GetRemainingListingDataAsyncTest()
        {
            await GetRemainingListingDataAsyncTest(WebSource.Indeed, TestResources.IndeedJobString);
        }

        [Test]
        public override Task GetUrlsAsyncTest()
        {
            return base.GetUrlsAsyncTest();
        }
    }
}