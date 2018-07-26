using System;
using JobSearcher.Interfaces;
using JobSearcher.UrlFetchers;

namespace JobSearcherTest.Mocks
{
    internal class IndeedUrlFetcherMock : IndeedUrlFetcher
    {
        #region Constructors

        public IndeedUrlFetcherMock(Func<IWebClient> webClientFactory) : base(webClientFactory)
        {
        }

        #endregion

        #region Properties

        public IHtmlParser MyParser => Parser;

        #endregion
    }
}