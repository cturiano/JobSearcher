using System.ComponentModel;
using JobSearcherTest.Mocks;
using NUnit.Framework;

namespace JobSearcherTest.AbstractTests
{
    [TestFixture]
    internal class BindableBaseTests
    {
        [Test]
        public void GetSetTest()
        {
            var bb = new BindableBaseMock
                     {
                         Prop1 = "test",
                         Prop2 = 37
                     };

            Assert.AreEqual(bb.Prop1, "test");
            Assert.AreEqual(bb.Prop2, 37);
        }

        [Test]
        public void OnPropertyChangedTest()
        {
            void Prop1Delegate(object sender, PropertyChangedEventArgs args)
            {
                Assert.AreEqual(args.PropertyName, nameof(BindableBaseMock.Prop1));
                Assert.AreEqual(((BindableBaseMock)sender).Prop1, "test");
            }

            void Prop2Delegate(object sender, PropertyChangedEventArgs args)
            {
                Assert.AreEqual(args.PropertyName, nameof(BindableBaseMock.Prop2));
                Assert.AreEqual(((BindableBaseMock)sender).Prop2, 37);
            }

            var bb = new BindableBaseMock();

            bb.PropertyChanged += Prop1Delegate;
            bb.Prop1 = "test";
            bb.PropertyChanged -= Prop1Delegate;

            bb.PropertyChanged += Prop2Delegate;
            bb.Prop2 = 37;
            bb.PropertyChanged -= Prop2Delegate;
        }
    }
}