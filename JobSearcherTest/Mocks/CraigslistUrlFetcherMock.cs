using System;
using JobSearcher.Interfaces;
using JobSearcher.UrlFetchers;

namespace JobSearcherTest.Mocks
{
    internal class CraigslistUrlFetcherMock : CraigslistUrlFetcher
    {
        #region Constructors

        public CraigslistUrlFetcherMock(Func<IWebClient> webClientFactory) : base(webClientFactory)
        {
        }

        #endregion

        #region Properties

        public IHtmlParser MyParser => Parser;

        #endregion
    }
}