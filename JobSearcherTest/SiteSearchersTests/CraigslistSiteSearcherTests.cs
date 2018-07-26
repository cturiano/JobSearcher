using System.Threading.Tasks;
using JobSearcherTest.Properties;
using JobSearcher.Utils;
using NUnit.Framework;

namespace JobSearcherTest.SiteSearchersTests
{
    [TestFixture]
    internal class CraigslistSiteSearcherTests : SiteSearcherTestsBase
    {
        [Test]
        public void ConstructorsAndPropertiesTest()
        {
            ConstructorsAndPropertiesTest(WebSource.Craigslist, Resources.CraigslistListFileString);
        }

        [Test]
        public async Task SearchAsyncTest()
        {
            // not really a good test.  CL has another layer of looping so the results are not created
            await SearchAsyncTest(WebSource.Craigslist, Resources.CraigslistCitiesFileString, 0);
        }
    }
}