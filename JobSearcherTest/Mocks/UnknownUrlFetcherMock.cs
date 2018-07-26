using System;
using JobSearcher.Interfaces;
using JobSearcher.UrlFetchers;

namespace JobSearcherTest.Mocks
{
    internal class UnknownUrlFetcherMock : UnknownUrlFetcher
    {
        #region Constructors

        public UnknownUrlFetcherMock(Func<IWebClient> webClientFactory) : base(webClientFactory)
        {
        }

        #endregion

        #region Properties

        public IHtmlParser MyParser => Parser;

        #endregion
    }
}