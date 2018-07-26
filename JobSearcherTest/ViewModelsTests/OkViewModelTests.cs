using System;
using JobSearcher.ViewModels;
using NUnit.Framework;

namespace JobSearcherTest.ViewModelsTests
{
    [TestFixture]
    internal class OkViewModelTests
    {
        [Test]
        public void CanOkTest()
        {
            var ok = new OkViewModel("message", "heading");
            Assert.IsTrue(ok.OkCommand.CanExecute(new object()));

            ok = new OkViewModel(string.Empty, string.Empty);    
            Assert.IsFalse(ok.OkCommand.CanExecute(new object()));
        }

        [Test]
        public void ConstructorsAndPropertiesTest()
        {
            var ok = new OkViewModel("message", "heading");
            Assert.AreEqual("message", ok.Message);
            Assert.AreEqual("heading", ok.Heading);
            Assert.IsNotNull(ok.OkCommand);
        }

        [Test]
        public void OkTest()
        {
            try
            {
                var ok = new OkViewModel("message", "heading");
                ok.OkCommand.Execute(new object());
            }
            catch (Exception e)
            {                  
                Assert.Fail(e.Message);
            }
        }
    }
}