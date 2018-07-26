using System.Threading.Tasks;
using JobSearcher.HtmlParsers;
using JobSearcherTest.Properties;
using NUnit.Framework;

namespace JobSearcherTest.HtmlParsersTests
{
    [TestFixture]
    internal class StackOverflowHtmlParserTests : HtmlParserTestBase<StackOverflowHtmlParser>
    {
        [Test]
        public async Task ParseListingDataAsyncTest()
        {
            if (Listings == null || Listings.Count <= 0)
            {
                await ParseListingsAsyncTest();
            }    
              
            Assert.IsTrue(string.IsNullOrEmpty(Listings[0].EmploymentType));

            var listing = await base.ParseListingDataAsyncTest(Resources.StackOverflowJobString);

            Assert.IsNotNull(listing);
            Assert.IsTrue(!string.IsNullOrEmpty(listing.EmploymentType));
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
            await base.ParseListingsAsyncTest(Resources.StackOverflowListFileString, 25);

            foreach (var listing in Listings)
            {
                Assert.IsNotNull(listing.ListingUrl);
                Assert.IsTrue(!string.IsNullOrEmpty(listing.Company));
                Assert.IsTrue(!string.IsNullOrEmpty(listing.City));
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