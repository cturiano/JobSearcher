using System.Globalization;
using System.Windows;
using JobSearcher.Utils;
using NUnit.Framework;

namespace JobSearcherTest.UtilsTests
{
    [TestFixture]
    public class VisibilityToBooleanConverterTests
    {
        private VisibilityToBooleanConverter _converter;

        [OneTimeSetUp]
        public void InitTests()
        {
            _converter = new VisibilityToBooleanConverter();
        }

        [Test]
        public void ConvertBackTest()
        {
            Assert.AreEqual(Visibility.Collapsed, _converter.ConvertBack(null, typeof(Visibility), null, CultureInfo.CurrentCulture));
            Assert.AreEqual(Visibility.Collapsed, _converter.ConvertBack(false, typeof(Visibility), null, CultureInfo.CurrentCulture));
            Assert.AreEqual(Visibility.Visible, _converter.ConvertBack(true, typeof(Visibility), null, CultureInfo.CurrentCulture));
        }

        [Test]
        public void ConvertTest()
        {
            if (_converter != null)
            {
                Assert.IsFalse((bool?)_converter.Convert(null, typeof(bool), null, CultureInfo.CurrentCulture));
                Assert.IsFalse((bool?)_converter.Convert(Visibility.Hidden, typeof(bool), null, CultureInfo.CurrentCulture));
                Assert.IsFalse((bool?)_converter.Convert(Visibility.Collapsed, typeof(bool), null, CultureInfo.CurrentCulture));
                Assert.IsTrue((bool?)_converter.Convert(Visibility.Visible, typeof(bool), null, CultureInfo.CurrentCulture));
            }
        }
    }
}