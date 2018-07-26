using System.Threading.Tasks;
using JobSearcherTest.Properties;
using JobSearcher.Utils;
using NUnit.Framework;

namespace JobSearcherTest.SiteSearchersTests
{
    [TestFixture]
    internal class IndeedSiteSearcherTests : SiteSearcherTestsBase
    {
        [Test]
        public void ConstructorsAndPropertiesTest()
        {
            ConstructorsAndPropertiesTest(WebSource.Indeed, Resources.IndeedListFileString);
        }

        [Test]
        public async Task SearchAsyncTest()
        {
            await SearchAsyncTest(WebSource.Indeed, Resources.IndeedListFileString, 11);
        }
    }
}