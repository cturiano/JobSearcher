using JobSearcher.ViewModels;
using NUnit.Framework;

namespace JobSearcherTest.ViewModelsTests
{
    [TestFixture]
    internal class OpenFileViewModelTests
    {
        [Test]
        public void ConstructorsAndPropertiesTest()
        {
            var open = new OpenFileViewModel("heading");
            Assert.AreEqual("heading", open.Heading);
            Assert.IsNotNull(open.Paths);
            Assert.AreEqual(0, open.Paths.Count);
        }
    }
}