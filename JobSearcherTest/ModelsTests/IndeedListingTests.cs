using JobSearcher.Utils;
using JobSearcherTest.Properties;
using NUnit.Framework;

namespace JobSearcherTest.ModelsTests
{
    [TestFixture]
    internal class IndeedListingTests : ListingTestsBase
    {
        [Test]
        public void ConstructorAndPropertiesTests()
        {
            ConstructorAndPropertiesTests(WebSource.Indeed, Resources.IndeedListFileString, "//div[contains(@data-tn-component,'organicJob')]");
        }

        [Test]
        public void ParseRemainingDataTest()
        {
            ParseRemainingDataTest(WebSource.Indeed, Resources.IndeedListFileString, Resources.IndeedJobString, "//div[contains(@data-tn-component,'organicJob')]", "//span[contains(@class, 'summary')]");
        }
    }
}