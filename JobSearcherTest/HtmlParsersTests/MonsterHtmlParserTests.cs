using System.Threading.Tasks;
using JobSearcher.HtmlParsers;
using JobSearcherTest.Properties;
using NUnit.Framework;

namespace JobSearcherTest.HtmlParsersTests
{
    [TestFixture]
    internal class MonsterHtmlParserTests : HtmlParserTestBase<MonsterHtmlParser>
    {
        [Test]
        public async Task ParseListingDataAsyncTest()
        {
            if (Listings == null || Listings.Count <= 0)
            {
                await ParseListingsAsyncTest();
            }

            var listing = await base.ParseListingDataAsyncTest(Resources.MonsterJobString);

            Assert.IsNotNull(listing);
            Assert.IsTrue(!string.IsNullOrEmpty(listing.Description));
        }

        [Test]
        public override Task ParseListingReplyAsyncTest()
        {
            return base.ParseListingReplyAsyncTest();
        }

        [Test]
        public async Task ParseListingsAsyncTest()
        {
            await base.ParseListingsAsyncTest(Resources.MonsterListFileString, 25);

            foreach (var listing in Listings)
            {
                Assert.IsNotNull(listing.ListingUrl);
                Assert.IsNotNull(listing.ListingTime);
                Assert.IsTrue(!string.IsNullOrEmpty(listing.Heading));
            }
        }

        [Test]
        public override Task ParseListItemsAsync()
        {
            return base.ParseListItemsAsync();
        }

        [Test]
        public override Task ParseReplyLinkAsyncTest()
        {
            return base.ParseReplyLinkAsyncTest();
        }
    }
}