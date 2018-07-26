using System.Threading.Tasks;
using JobSearcher.Utils;
using JobSearcherTest.Properties;
using NUnit.Framework;

namespace JobSearcherTest.SiteSearchersTests
{
    [TestFixture]
    internal class MonsterSiteSearcherTests : SiteSearcherTestsBase
    {
        [Test]
        public void ConstructorsAndPropertiesTest()
        {
            ConstructorsAndPropertiesTest(WebSource.Monster, Resources.MonsterListFileString);
        }

        [Test]
        public async Task SearchAsyncTest()
        {
            await SearchAsyncTest(WebSource.Monster, Resources.MonsterListFileString, 25);
        }
    }
}