using System.Net.Mail;
using JobSearcher.Abstract;
using JobSearcher.Models;
using NUnit.Framework;

namespace JobSearcherTest.ModelsTests
{
    [TestFixture]
    internal class EmailInfoTests
    {
        private class TestListing : ListingBase
        {
        }

        [Test]
        public void ConstructorAndPropertiesTest()
        {
            var e = new EmailInfo();
            Assert.AreEqual("https://denver.craigslist.org", e.Body);
            Assert.AreEqual(new MailAddress("cturiano@gmail.com"), e.FromAddress);
            Assert.AreEqual(new MailAddress("cturiano@gmail.com"), e.ToAddress);
            Assert.AreEqual("Big Money Employment Opportunity", e.Subject);
        }

        [Test]
        public void EqualsTest()
        {
            var e = new EmailInfo();
            var f = new EmailInfo();
            var l = new TestListing();

            Assert.IsFalse(e.Equals(l));
            Assert.IsFalse(e.Equals((object)null));
            Assert.IsTrue(e.Equals((object)e));

            Assert.IsTrue(e.Equals(f));
            Assert.IsTrue(e.Equals(e));
            Assert.IsFalse(e.Equals(null));
        }

        [Test]
        public void GetHashCodeTest()
        {
            var e = new EmailInfo();
            var f = new EmailInfo {Body = "body"};

            Assert.AreEqual(e.GetHashCode(), e.GetHashCode());
            Assert.AreNotEqual(e.GetHashCode(), f.GetHashCode());
        }

        [Test]
        public void ToStringTest()
        {
            var e = new EmailInfo();
            Assert.AreEqual("cturiano@gmail.com", e.ToString());
        }
    }
}