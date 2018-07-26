using System;
using JobSearcher.Utils;
using JobSearcherTest.Properties;
using NUnit.Framework;

namespace JobSearcherTest.ModelsTests
{
    [TestFixture]
    internal class DiceListingTests : ListingTestsBase
    {  
        [Test]
        public void ConstructorAndPropertiesTests()
        {
            ConstructorAndPropertiesTests(WebSource.Dice, Resources.DiceListFileString, "//div[contains(@class,'complete-serp-result-div')]");
        }

        [Test]
        public void ParseRemainingDataTest()
        {
            ParseRemainingDataTest(WebSource.Dice, Resources.DiceListFileString, Resources.DiceJobString, "//div[contains(@class,'complete-serp-result-div')]", "//div[contains(@itemprop, 'description')]");
        }
    }
}