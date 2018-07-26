using System.Threading.Tasks;
using JobSearcher.HtmlParsers;
using JobSearcher.Utils;
using JobSearcherTest.Mocks;
using JobSearcherTest.Properties;
using NUnit.Framework;

namespace JobSearcherTest.UrlFetchersTests
{
    [TestFixture]
    internal class DiceUrlFetcherTests : UrlFetcherTestsBase
    {
        [Test]
        public void ConstructorsAndPropertiesTest()
        {
            var fetcher = ConstructorsAndPropertiesTest(WebSource.Dice, Resources.DiceListFileString) as DiceUrlFetcherMock;
            Assert.IsNotNull(fetcher.MyParser);
            Assert.IsInstanceOf<DiceHtmlParser>(fetcher.MyParser);
        }

        [Test]
        public async Task GetListingsAsyncTest()
        {
            await GetListingsAsyncTest(WebSource.Dice, Resources.DiceListFileString, JobSearcher.Properties.Resources.DiceBaseUrl, JobSearcher.Properties.Resources.DiceQueryString, 40);
        }

        [Test]
        public async Task GetRemainingListingDataAsyncTest()
        {
            await GetRemainingListingDataAsyncTest(WebSource.Dice, Resources.DiceJobString);
        }

        [Test]
        public override Task GetUrlsAsyncTest()
        {
            return base.GetUrlsAsyncTest();
        }
    }
}