using JobSearcher.Utils;
using JobSearcherTest.Properties;
using NUnit.Framework;

namespace JobSearcherTest.ModelsTests
{
    [TestFixture]
    internal class StackOverflowListingTests : ListingTestsBase
    {
        [Test]
        public void ConstructorAndPropertiesTests()
        {
            ConstructorAndPropertiesTests(WebSource.StackOverflow, Resources.StackOverflowListFileString, "//div[contains(@class, 'job-summary')]");
            Assert.IsTrue(!string.IsNullOrEmpty(Listing.SalaryRange));
        }

        [Test]
        public void ParseRemainingDataTest()
        {
            ParseRemainingDataTest(WebSource.StackOverflow, Resources.StackOverflowListFileString, Resources.StackOverflowJobString, "//div[contains(@class, 'job-summary')]", "//div[contains(@id, 'job-detail')]");
            Assert.IsTrue(!string.IsNullOrEmpty(Listing.EmploymentType));
        }
    }
}