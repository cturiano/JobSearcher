using JobSearcher.Models;
using NUnit.Framework;

namespace JobSearcherTest.ModelsTests
{
    [TestFixture]
    internal class CredentialsTests
    {
        [Test]
        public void ConstructorAndPropertiesTest()
        {
            var c = new Credentials();
            Assert.AreEqual(string.Empty, c.Password);
            Assert.AreEqual(string.Empty, c.UserName);
            c.Password = "pw";
            c.UserName = "un";
            Assert.AreEqual("pw", c.Password);
            Assert.AreEqual("un", c.UserName);

            c = new Credentials("un", "pw");
            Assert.AreEqual("pw", c.Password);
            Assert.AreEqual("un", c.UserName);
        }
    }
}