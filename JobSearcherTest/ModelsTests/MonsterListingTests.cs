using JobSearcher.Utils;
using JobSearcherTest.Properties;
using NUnit.Framework;

namespace JobSearcherTest.ModelsTests
{
    [TestFixture]
    internal class MonsterListingTests : ListingTestsBase
    {
        [Test]
        public void ConstructorAndPropertiesTests()
        {
            ConstructorAndPropertiesTests(WebSource.Monster, Resources.MonsterListFileString, "//div[contains(@class,'js_result_container clearfix primary')]");
        }

        [Test]
        public void ParseRemainingDataTest()
        {
            ParseRemainingDataTest(WebSource.Monster, Resources.MonsterListFileString, Resources.MonsterJobString, "//div[contains(@class,'js_result_container clearfix primary')]", "//div[contains(@id, 'JobViewContent')]");
        }
    }
}