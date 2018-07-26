using JobSearcher.Abstract;
using JobSearcher.Services;
using NUnit.Framework;

namespace JobSearcherTest.AbstractTests
{
    [TestFixture]
    internal class DialogViewModelTests
    {
        private class TestDialogViewModel : DialogViewModel
        {
            #region Constructors

            public TestDialogViewModel(string message, string heading) : base(message, heading)
            {
            }

            #endregion
        }

        [Test]
        public void ConstructorsAndPropertiesTests()
        {
            var bb = new TestDialogViewModel("message", "heading");
            Assert.AreEqual(bb.Heading, "heading");
            Assert.AreEqual(bb.Message, "message");

            bb.CloseDialogWithResult(null, DialogResult.OK);
            Assert.AreEqual(DialogResult.OK, bb.UserDialogResult);
        }
    }
}