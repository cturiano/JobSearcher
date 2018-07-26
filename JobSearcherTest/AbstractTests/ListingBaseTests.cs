using JobSearcher.Abstract;
using JobSearcher.Models;
using JobSearcherTest.Mocks;
using NUnit.Framework;

namespace JobSearcherTest.AbstractTests
{
    [TestFixture]
    internal class ListingBaseTests
    {
        [Test]
        public void ConstructorAndPropertiesTest()
        {
            var l = new ListingBaseMock();
            Assert.AreEqual("cturiano@gmail.com", l.ApplyEmail.ToString());
            Assert.AreEqual("Gotham", l.City);
            Assert.AreEqual("Wayne Industries", l.Company);
            Assert.AreEqual("This is the description", l.Description);
            Assert.AreEqual("Full Time", l.EmploymentType);
            Assert.AreEqual("This is the heading", l.Heading);
            Assert.AreEqual("http://www.google.com/", l.ListingUrl.AbsoluteUri);
            Assert.AreEqual("big money", l.SalaryRange);
            Assert.IsFalse(l.Selected);
        }

        [Test]
        public void EqualsTest()
        {
            var e = new EmailInfo();
            var l = new ListingBaseMock();
            var m = new ListingBaseMock();

            Assert.IsFalse(l.Equals((BindableBase)e));
            Assert.IsFalse(l.Equals((object)null));
            Assert.IsTrue(l.Equals((object)m));

            Assert.IsTrue(l.Equals(m));
            Assert.IsTrue(l.Equals(l));
            Assert.IsFalse(l.Equals((EmailInfo)null));
        }

        [Test]
        public void GetHashCodeTest()
        {
            var e = new ListingBaseMock();
            var f = new ListingBaseMock {City = "city"};

            Assert.AreEqual(e.GetHashCode(), e.GetHashCode());
            Assert.AreNotEqual(e.GetHashCode(), f.GetHashCode());
        }
    }
}