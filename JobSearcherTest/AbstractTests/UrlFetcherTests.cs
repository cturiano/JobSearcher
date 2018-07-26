using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JobSearcher.Abstract;
using JobSearcher.HtmlParsers;
using JobSearcher.Interfaces;
using JobSearcherTest.Mocks;
using NUnit.Framework;

namespace JobSearcherTest.AbstractTests
{
    [TestFixture]
    internal class UrlFetcherTests
    {
        private class TestUrlFetcher : UrlFetcher
        {
            #region Constructors

            public TestUrlFetcher(string url, string urlQueryString) : base(url, urlQueryString, () => new WebClientMock(""))
            {
                Parser = new CraigslistHtmlParser();
            }

            #endregion

            #region Public Methods

            public override Task<IEnumerable<ListingBase>> GetListingsAsync(Uri url, CancellationToken ct)
            {
                throw new NotImplementedException();
            }

            public IHtmlParser GetParser()
            {
                return Parser;
            }

            public override Task<ListingBase> GetRemainingListingDataAsync(ListingBase listing, CancellationToken ct)
            {
                throw new NotImplementedException();
            }

            public override Task<IEnumerable<Uri>> GetUrlsAsync(CancellationToken ct)
            {
                throw new NotImplementedException();
            }

            #endregion
        }

        [Test]
        public void ConstructorAndPropertiesTest()
        {
            const string url = "https://www.gooogle.com";
            const string qs = "the query string";
            var tuf = new TestUrlFetcher(url, qs);
            Assert.AreEqual(url, tuf.URL);
            Assert.AreEqual(qs, tuf.UrlQueryString);
            Assert.IsInstanceOf<CraigslistHtmlParser>(tuf.GetParser());
        }
    }
}