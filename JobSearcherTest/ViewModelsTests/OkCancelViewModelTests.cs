using System;
using JobSearcher.ViewModels;
using NUnit.Framework;

namespace JobSearcherTest.ViewModelsTests
{
    [TestFixture]
    internal class OkCancelViewModelTests
    {
        [Test]
        public void CancelTest()
        {
            try
            {                                                    
                var ok = new OkCancelViewModel("message", "heading");
                ok.CancelCommand.Execute(new object());
            }
            catch (Exception e)
            {      
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void ConstructorsAndPropertiesTest()
        {                                               
            var ok = new OkCancelViewModel("message", "heading");
            Assert.AreEqual("message", ok.Message);
            Assert.AreEqual("heading", ok.Heading);
            Assert.IsNotNull(ok.CancelCommand);
        }
    }
}