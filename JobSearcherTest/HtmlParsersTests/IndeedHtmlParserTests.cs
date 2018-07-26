using System.Threading.Tasks;
using JobSearcher.HtmlParsers;
using JobSearcherTest.Properties;
using NUnit.Framework;

namespace JobSearcherTest.HtmlParsersTests
{
    [TestFixture]
    internal class IndeedHtmlParserTests : HtmlParserTestBase<IndeedHtmlParser>
    {
        [Test]
        public async Task ParseListingDataAsyncTest()
        {
            if (Listings == null || Listings.Count <= 0)
            {
                await ParseListingsAsyncTest();
            }
                                                                          
            var listing = await base.ParseListingDataAsyncTest(Resources.IndeedJobString);

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
            await base.ParseListingsAsyncTest(Resources.IndeedListFileString, 11);

            foreach (var listing in Listings)
            {
                Assert.IsNotNull(listing.ListingUrl);
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