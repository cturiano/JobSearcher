using System.Threading.Tasks;
using JobSearcher.Utils;
using JobSearcherTest.Properties;
using NUnit.Framework;

namespace JobSearcherTest.SiteSearchersTests
{
    [TestFixture]
    internal class DiceSiteSearcherTests : SiteSearcherTestsBase
    {
        [Test]
        public void ConstructorsAndPropertiesTest()
        {
            ConstructorsAndPropertiesTest(WebSource.Dice, Resources.DiceListFileString);
        }

        [Test]
        public async Task SearchAsyncTest()
        {
            await SearchAsyncTest(WebSource.Dice, Resources.DiceListFileString, 40);
        }
    }
}