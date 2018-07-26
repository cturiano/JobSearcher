using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JobSearcher.HtmlParsers;
using JobSearcherTest.Properties;
using NUnit.Framework;

namespace JobSearcherTest.HtmlParsersTests
{
    [TestFixture]
    internal class CraigslistHtmlParserTests : HtmlParserTestBase<CraigslistHtmlParser>
    {
        [Test]
        public async Task ParseListingDataAsyncTest()
        {
            if (Listings == null || Listings.Count <= 0)
            {
                await ParseListingsAsyncTest();
            }
                               
            Assert.IsTrue(string.IsNullOrEmpty(Listings[0].EmploymentType));
            Assert.IsTrue(string.IsNullOrEmpty(Listings[0].SalaryRange));

            var listing = await base.ParseListingDataAsyncTest(Resources.CraigslistJobString);

            Assert.IsNotNull(listing);
            Assert.IsTrue(!string.IsNullOrEmpty(listing.SalaryRange));
            Assert.IsTrue(!string.IsNullOrEmpty(listing.EmploymentType));
            Assert.IsTrue(!string.IsNullOrEmpty(listing.Description));
        }

        [Test]
        public override async Task ParseListingReplyAsyncTest()
        {     
            if (Listings == null || Listings.Count <= 0)
            {
                await ParseListingsAsyncTest();
            }
            
            var html = HelperMethods.LoadFromFile(Resources.CraigslistReplyFileString);
            if (!string.IsNullOrEmpty(html))
            {
                await Parser.ParseListingReplyAsync(html, Listings[0], CancellationToken.None);
                Assert.IsNotNull(Listings[0].ApplyEmail);                                       
                Assert.IsNotNull(Listings[0].ApplyEmail.ToAddress);
                Assert.IsTrue(!string.IsNullOrEmpty(Listings[0].ApplyEmail.Body));
                Assert.IsTrue(!string.IsNullOrEmpty(Listings[0].ApplyEmail.Subject));
            }
            else
            {
                Assert.Fail("html should not be null.");
            }
        }

        [Test]
        public async Task ParseListingsAsyncTest()
        {
            await base.ParseListingsAsyncTest(Resources.CraigslistListFileString, 120);

            foreach (var listing in Listings)
            {
                Assert.IsNotNull(listing.ListingUrl);
                Assert.IsNotNull(listing.ListingTime);
                Assert.IsTrue(!string.IsNullOrEmpty(listing.Heading));
            }
        }

        [Test]
        public override async Task ParseListItemsAsync()
        {   
            var html = HelperMethods.LoadFromFile(Resources.CraigslistCitiesFileString);
            if (!string.IsNullOrEmpty(html))
            {
                var cityLinks = await Parser.ParseListItemsAsync(html, CancellationToken.None);

                Assert.IsNotNull(cityLinks);
                Assert.AreEqual(715, cityLinks.Count());
            }
            else
            {
                Assert.Fail("html should not be null.");
            }
        }

        [Test]
        public override async Task ParseReplyLinkAsyncTest()
        {
            var html = HelperMethods.LoadFromFile(Resources.CraigslistJobString);
            if (!string.IsNullOrEmpty(html))
            {
                var replyLink = await Parser.ParseReplyLinkAsync(html, CancellationToken.None);

                Assert.IsTrue(!string.IsNullOrEmpty(replyLink));
            }
            else
            {
                Assert.Fail("html should not be null.");
            }
        }
    }
}