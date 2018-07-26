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
    internal class StackOverflowUrlFetcherTests : UrlFetcherTestsBase
    {
        [Test]
        public void ConstructorsAndPropertiesTest()
        {
            var fetcher = ConstructorsAndPropertiesTest(WebSource.StackOverflow, TestResources.StackOverflowListFileString) as StackOverflowUrlFetcherMock;
            Assert.IsNotNull(fetcher.MyParser);
            Assert.IsInstanceOf<StackOverflowHtmlParser>(fetcher.MyParser);
        }

        [Test]
        public async Task GetListingsAsyncTest()
        {
            await GetListingsAsyncTest(WebSource.StackOverflow, TestResources.StackOverflowListFileString, Resources.StackOverFlowBaseUrl, Resources.StackOverFlowQueryString, 25);
        }

        [Test]
        public async Task GetRemainingListingDataAsyncTest()
        {
            await GetRemainingListingDataAsyncTest(WebSource.StackOverflow, TestResources.StackOverflowJobString);
        }

        [Test]
        public override Task GetUrlsAsyncTest()
        {
            return base.GetUrlsAsyncTest();
        }
    }
}