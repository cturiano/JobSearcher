using System;
using JobSearcher.Interfaces;
using JobSearcher.UrlFetchers;

namespace JobSearcherTest.Mocks
{
    internal class StackOverflowUrlFetcherMock : StackOverflowUrlFetcher
    {
        #region Constructors

        public StackOverflowUrlFetcherMock(Func<IWebClient> webClientFactory) : base(webClientFactory)
        {
        }

        #endregion

        #region Properties

        public IHtmlParser MyParser => Parser;

        #endregion
    }
}