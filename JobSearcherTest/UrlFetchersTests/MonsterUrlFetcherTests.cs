using System;
using System.Threading.Tasks;
using JobSearcher.HtmlParsers;
using JobSearcher.Utils;
using JobSearcherTest.Mocks;
using JobSearcherTest.Properties;
using NUnit.Framework;

namespace JobSearcherTest.UrlFetchersTests
{
    [TestFixture]
    internal class MonsterUrlFetcherTests : UrlFetcherTestsBase
    {
        [Test]
        public void ConstructorsAndPropertiesTest()
        {
            var fetcher = ConstructorsAndPropertiesTest(WebSource.Monster, Resources.MonsterListFileString) as MonsterUrlFetcherMock;
            Assert.IsNotNull(fetcher.MyParser);
            Assert.IsInstanceOf<MonsterHtmlParser>(fetcher.MyParser);
        }

        [Test]
        public async Task GetListingsAsyncTest()
        {
            await GetListingsAsyncTest(WebSource.Monster, Resources.MonsterListFileString, JobSearcher.Properties.Resources.MonsterBaseUrl, JobSearcher.Properties.Resources.MonsterQueryString, 25);
        }

        [Test]
        public async Task GetRemainingListingDataAsyncTest()
        {
            await GetRemainingListingDataAsyncTest(WebSource.Monster, Resources.MonsterJobString);
        }

        [Test]
        public override Task GetUrlsAsyncTest()
        {
            return base.GetUrlsAsyncTest();
        }
    }
}