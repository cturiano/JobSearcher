using HtmlAgilityPack;
using JobSearcher.Abstract;
using JobSearcher.Utils;
using NUnit.Framework;

namespace JobSearcherTest.ModelsTests
{
    internal class ListingTestsBase
    {
        #region Fields

        private HtmlNode _node;
        protected ListingBase Listing;

        #endregion

        #region Public Methods

        public void ConstructorAndPropertiesTests(WebSource webSource, string listFileString, string xPath)
        {
            var html = HelperMethods.LoadFromFile(listFileString);
            if (!string.IsNullOrEmpty(html))
            {
                _node = HelperMethods.GetNodeFromHtml(html, xPath);

                if (_node != null)
                {
                    Listing = HelperMethods.ListingFactory(webSource, _node);

                    Assert.IsNotNull(Listing.ListingUrl);
                    if (webSource != WebSource.Craigslist)
                    {
                        Assert.IsTrue(!string.IsNullOrEmpty(Listing.Company));
                        Assert.IsTrue(!string.IsNullOrEmpty(Listing.City));
                    }

                    Assert.IsNotNull(Listing.ListingTime);
                    Assert.IsTrue(!string.IsNullOrEmpty(Listing.Heading));
                }
                else
                {
                    Assert.Fail("Node should not be null.");
                }
            }
            else
            {
                Assert.Fail("html should not be null.");
            }
        }

        public void ParseRemainingDataTest(WebSource webSource, string listFileString, string jobString, string listXPath, string jobXPath)
        {
            if (Listing == null)
            {
                ConstructorAndPropertiesTests(webSource, listFileString, listXPath);
            }

            var html = HelperMethods.LoadFromFile(jobString);
            if (!string.IsNullOrEmpty(html))
            {
                _node = HelperMethods.GetNodeFromHtml(html, jobXPath);

                if (_node != null)
                {
                    Listing.ParseRemainingData(_node);
                    Assert.IsTrue(!string.IsNullOrEmpty(Listing.Description));
                    if (webSource != WebSource.Indeed)
                    {
                        Assert.IsTrue(!string.IsNullOrEmpty(Listing.EmploymentType));
                    }
                }
                else
                {
                    Assert.Fail("Node should not be null.");
                }
            }
            else
            {
                Assert.Fail("html should not be null.");
            }
        }

        #endregion
    }
}