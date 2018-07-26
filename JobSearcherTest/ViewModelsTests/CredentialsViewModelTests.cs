using JobSearcher.Models;
using JobSearcher.ViewModels;
using NUnit.Framework;

namespace JobSearcherTest.ViewModelsTests
{
    [TestFixture]
    internal class CredentialsViewModelTests
    {
        [Test]
        public void CancelTest()
        {                            
            var c = new CredentialsViewModel("message", "heading") {Credentials = new Credentials("username", "password")};
            c.CancelCommand.Execute(new object());
            Assert.IsNull(c.Credentials);
        }

        [Test]
        public void CanOkTest()
        {
            var c = new CredentialsViewModel("message", "heading") {Credentials = new Credentials("username", "password")};
            Assert.IsTrue(c.OkCommand.CanExecute(new object()));
            
            c = new CredentialsViewModel(string.Empty, string.Empty);
            Assert.IsFalse(c.OkCommand.CanExecute(new object()));

            c.Credentials = new Credentials("username", "password");
            Assert.IsFalse(c.OkCommand.CanExecute(new object()));
        }

        [Test]
        public void ConstructorsAndPropertiesTest()
        {        
            var c = new CredentialsViewModel("message", "heading");
            Assert.AreEqual(new Credentials(), c.Credentials);
            Assert.AreEqual("message", c.Message);
            Assert.AreEqual("heading", c.Heading);
        }
    }
}