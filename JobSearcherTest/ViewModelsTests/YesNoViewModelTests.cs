using JobSearcher.ViewModels;
using NUnit.Framework;

namespace JobSearcherTest.ViewModelsTests
{
    [TestFixture]
    internal class YesNoViewModelTests
    {
        [Test]
        public void ConstructorsAndPropertiesTest()
        {
            var yes = new YesNoViewModel("message", "heading");
            Assert.AreEqual("message", yes.Message);
            Assert.AreEqual("heading", yes.Heading);
            Assert.IsNotNull(yes.NoCommand);
            Assert.IsNotNull(yes.YesCommand);
        }
    }
}