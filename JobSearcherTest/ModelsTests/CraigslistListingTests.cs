using JobSearcher.Utils;
using JobSearcherTest.Properties;
using NUnit.Framework;

namespace JobSearcherTest.ModelsTests
{
    [TestFixture]
    internal class CraigslistListingTests : ListingTestsBase
    {
        [Test]
        public void ConstructorAndPropertiesTests()
        {
            ConstructorAndPropertiesTests(WebSource.Craigslist, Resources.CraigslistListFileString, "//ul/li[contains(@class,'result-row')]");
        }

        [Test]
        public void ParseRemainingDataTest()
        {
            ParseRemainingDataTest(WebSource.Craigslist, Resources.CraigslistListFileString, Resources.CraigslistJobString, "//ul/li[contains(@class,'result-row')]", "//section[contains(@class,'userbody')]");
        }
    }
}