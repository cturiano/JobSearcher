using System.Threading.Tasks;
using JobSearcherTest.Properties;
using JobSearcher.Utils;
using NUnit.Framework;

namespace JobSearcherTest.SiteSearchersTests
{
    [TestFixture]
    internal class StackOverflowSiteSearcherTests : SiteSearcherTestsBase
    {
        [Test]
        public void ConstructorsAndPropertiesTest()
        {
            ConstructorsAndPropertiesTest(WebSource.StackOverflow, Resources.StackOverflowListFileString);
        }

        [Test]
        public async Task SearchAsyncTest()
        {
            await SearchAsyncTest(WebSource.StackOverflow, Resources.StackOverflowListFileString, 25);
        }
    }
}