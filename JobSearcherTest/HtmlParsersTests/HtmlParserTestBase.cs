using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JobSearcher.Abstract;
using JobSearcher.HtmlParsers;
using JobSearcher.Interfaces;
using JobSearcherTest.Mocks;
using JobSearcherTest.Properties;
using NUnit.Framework;

namespace JobSearcherTest.HtmlParsersTests
{
    internal class HtmlParserTestBase<T> where T : IHtmlParser, new()
    {
        #region Fields

        protected readonly IHtmlParser Parser = new T();
        protected List<ListingBase> Listings = new List<ListingBase>();

        #endregion

        #region Public Methods

        public virtual async Task<ListingBase> ParseListingDataAsyncTest(string jobString)
        {
            var html = HelperMethods.LoadFromFile(jobString);
            if (!string.IsNullOrEmpty(html))
            {
                Assert.IsNotNull(Listings[0]);
                Assert.IsTrue(string.IsNullOrEmpty(Listings[0].Description));

                return await Parser.ParseListingDataAsync(html, Listings[0], CancellationToken.None);
            }

            Assert.Fail("html should not be null.");
            return null;
        }

        public virtual Task ParseListingReplyAsyncTest()
        {
            var html = HelperMethods.LoadFromFile(Resources.IndeedListFileString);

            var parser = new StackOverflowHtmlParser();
            Assert.ThrowsAsync(typeof(NotImplementedException), async () => await parser.ParseListingReplyAsync(html, new ListingBaseMock(), CancellationToken.None));
            return Task.CompletedTask;
        }

        public virtual async Task ParseListingsAsyncTest(string listFileString, int count)
        {
            var html = HelperMethods.LoadFromFile(listFileString);
            if (!string.IsNullOrEmpty(html))
            {
                Listings = (await Parser.ParseListingsAsync(html, CancellationToken.None)).ToList();

                Assert.IsNotNull(Listings);
                Assert.AreEqual(count, Listings.Count);
            }
            else
            {
                Assert.Fail("html should not be null.");
            }
        }

        public virtual Task ParseListItemsAsync()
        {
            var html = HelperMethods.LoadFromFile(Resources.IndeedListFileString);

            var parser = new StackOverflowHtmlParser();
            Assert.ThrowsAsync(typeof(NotImplementedException), async () => await parser.ParseListItemsAsync(html, CancellationToken.None));
            return Task.CompletedTask;
        }

        public virtual Task ParseReplyLinkAsyncTest()
        {
            var html = HelperMethods.LoadFromFile(Resources.IndeedListFileString);

            var parser = new StackOverflowHtmlParser();
            Assert.ThrowsAsync(typeof(NotImplementedException), async () => await parser.ParseReplyLinkAsync(html, CancellationToken.None));
            return Task.CompletedTask;
        }

        #endregion
    }
}